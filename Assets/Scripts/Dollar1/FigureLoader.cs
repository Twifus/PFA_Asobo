using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using WobbrockLib;

using Dollar = Recognizer.Dollar;

public class FigureLoader {

    private Dollar.Recognizer recognizer;
    private string filePath = "./Assets/Figures/";
    private string[] figures = { "loop" };
    private string[] curves = { "height", "roll", "pitch", "yaw" };

	public FigureLoader(Dollar.Recognizer recognizer) {
        this.recognizer = recognizer;
    }

    public void LoadFigures() {
        foreach(string figure in figures) {
            foreach(string curve in curves) {
                // vérifier l'existence du fichier
                if (!File.Exists(filePath + figure + "_" + curve + ".xml")) {
                    CreateFigureFiles(figure);
                }
                recognizer.LoadGesture(filePath + figure + "_" + curve + ".xml");
            }
        }
    }

    // Créé tous les fichiers xml associés à la figure donnée
    private void CreateFigureFiles(string figure) {
        StreamReader file = new StreamReader(filePath + figure + ".csv");
        List<TimePointF> height = new List<TimePointF>();
        List<TimePointF> roll = new List<TimePointF>();
        List<TimePointF> pitch = new List<TimePointF>();
        List<TimePointF> yaw = new List<TimePointF>();
        
        // Lire le fichier et le parser
        int i = 0;
        do {
            string textLine = file.ReadLine();
            float[] values = Array.ConvertAll<string, float>(textLine.Split(';'), new Converter<string, float>(StringToFloat));
            long time = (long)(values[0] * 1000); // values[0] est le temps en secondes
            height.Add(new TimePointF(i, values[1], time));
            roll.Add(new TimePointF(i, values[2], time));
            pitch.Add(new TimePointF(i, values[3], time));
            yaw.Add(new TimePointF(i, values[4], time));
            i++;
        } while (file.Peek() != -1);
        
        // Ecrire les fichiers de figures
        recognizer.SaveGesture(filePath + figure + "_" + "height" + ".xml", height);
        recognizer.SaveGesture(filePath + figure + "_" + "roll" + ".xml", roll);
        recognizer.SaveGesture(filePath + figure + "_" + "pitch" + ".xml", pitch);
        recognizer.SaveGesture(filePath + figure + "_" + "yaw" + ".xml", yaw);
    }

    // Convertisseur de string (au format "xx.xxx") en float
    private float StringToFloat(string s) {
        return float.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
    }
}

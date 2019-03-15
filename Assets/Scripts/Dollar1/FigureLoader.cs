using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using WobbrockLib;

public class FigureLoader {

    #region Members

    private Recognizer recognizerHeight;
    private Recognizer recognizerRoll;
    private Recognizer recognizerPitch;
    private Recognizer recognizerYaw;
    private readonly string filePath = "./Assets/Figures/";
    private string[] figures = { "Roll", "Loop" };
    private string[] curves = { "height", "roll", "pitch", "yaw" };

    #endregion

    #region Constructor

    public FigureLoader(Recognizer recognizerHeight, Recognizer recognizerRoll, Recognizer recognizerPitch, Recognizer recognizerYaw) {
        this.recognizerHeight = recognizerHeight;
        this.recognizerRoll = recognizerRoll;
        this.recognizerPitch = recognizerPitch;
        this.recognizerYaw = recognizerYaw;
    }

    #endregion

    #region Loader

    public void LoadFigures() {
        //foreach(string figure in figures) {
        //    foreach(string curve in curves) {
        //        // vérifier l'existence du fichier
        //        if (!File.Exists(filePath + figure + "_" + curve + ".xml")) {
        //            CreateFigureFiles(figure);
        //        }
        //        recognizer.LoadGesture(filePath + figure + "_" + curve + ".xml");
        //    }
        //}

        Loop();
        Roll();
        CubanEight();
    }

    #endregion

    #region File Parsing

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
        recognizerHeight.SaveGesture(filePath + figure + "_" + "height" + ".xml", height);
        recognizerRoll.SaveGesture(filePath + figure + "_" + "roll" + ".xml", roll);
        recognizerPitch.SaveGesture(filePath + figure + "_" + "pitch" + ".xml", pitch);
        recognizerYaw.SaveGesture(filePath + figure + "_" + "yaw" + ".xml", yaw);
    }


    private void CSVParser(StreamReader file, List<TimePointF> height, List<TimePointF> roll, List<TimePointF> pitch, List<TimePointF> yaw) {
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
    }


    // Convertisseur de string (au format "xx.xxx") en float
    private float StringToFloat(string s) {
        return float.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
    }

    #endregion

    #region Figures

    private void Loop() {
        StreamReader file = new StreamReader(filePath + "Loop" + "-Traj.csv");
        List<TimePointF> height = new List<TimePointF>();
        List<TimePointF> roll = new List<TimePointF>();
        List<TimePointF> pitch = new List<TimePointF>();
        List<TimePointF> yaw = new List<TimePointF>();

        // Lire le fichier et le parser
        CSVParser(file, height, roll, pitch, yaw);

        // Créer les figures
        recognizerHeight.CreateGesture("Bosse", height);
        recognizerRoll.CreateGesture("LigneCoupee", roll);
        recognizerPitch.CreateGesture("ZigZag", pitch);
        recognizerYaw.CreateGesture("LigneCoupee", yaw);
    }


    private void Roll() {
        StreamReader file = new StreamReader(filePath + "Roll" + "-Traj.csv");
        List<TimePointF> height = new List<TimePointF>();
        List<TimePointF> roll = new List<TimePointF>();
        List<TimePointF> pitch = new List<TimePointF>();
        List<TimePointF> yaw = new List<TimePointF>();

        // Lire le fichier et le parser
        CSVParser(file, height, roll, pitch, yaw);

        // Créer les figures
        recognizerHeight.CreateGesture("LigneDroite", height);
        recognizerRoll.CreateGesture("LigneMontante", roll);
        recognizerPitch.CreateGesture("LigneDroite", pitch);
        recognizerYaw.CreateGesture("LigneDroite", yaw);
    }


    private void CubanEight() {
        StreamReader file = new StreamReader(filePath + "CubanEight" + "-Traj.csv");
        List<TimePointF> height = new List<TimePointF>();
        List<TimePointF> roll = new List<TimePointF>();
        List<TimePointF> pitch = new List<TimePointF>();
        List<TimePointF> yaw = new List<TimePointF>();

        // Lire le fichier et le parser
        CSVParser(file, height, roll, pitch, yaw);

        // Créer les figures
        recognizerHeight.CreateGesture("DoubleBosse", height);
        recognizerRoll.CreateGesture("DoubleDemieLigneMontante", roll);
        recognizerPitch.CreateGesture("DoubleZigZag", pitch);
        //recognizer.CreateGesture("LigneCoupee", yaw);
    }

    #endregion
}

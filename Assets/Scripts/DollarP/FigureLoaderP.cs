using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using PDollarGestureRecognizer;

public class FigureLoaderP {

    #region Members

    private readonly string filePath = "./Assets/Figures/";
    private string[] figures = { "Roll", "Loop" };
    private string[] curves = { "height", "roll", "pitch", "yaw" };

    private Gesture[] gesturesHeight;
    private Gesture[] gesturesRoll;
    private Gesture[] gesturesPitch;
    private Gesture[] gesturesYaw;
    private int[] next = { 0, 0, 0, 0 };

    #endregion

    #region Loader

    public void LoadFigures(Gesture[] height, Gesture[] roll, Gesture[] pitch, Gesture[] yaw) {
        //foreach(string figure in figures) {
        //    foreach(string curve in curves) {
        //        // vérifier l'existence du fichier
        //        if (!File.Exists(filePath + figure + "_" + curve + ".xml")) {
        //            CreateFigureFiles(figure);
        //        }
        //        recognizer.LoadGesture(filePath + figure + "_" + curve + ".xml");
        //    }
        //}

        gesturesHeight = new Gesture[3];
        gesturesRoll = new Gesture[3];
        gesturesPitch = new Gesture[3];
        gesturesYaw = new Gesture[2];

        Loop();
        Roll();
        CubanEight();

        height = gesturesHeight;
        roll = gesturesRoll;
        pitch = gesturesPitch;
        yaw = gesturesYaw;
    }

    #endregion

    #region File Parsing

    // Créé tous les fichiers xml associés à la figure donnée
    private void CreateFigureFiles(string figure) {
        StreamReader file = new StreamReader(filePath + figure + ".csv");
        Point[] height = new Point[256];
        Point[] roll = new Point[256];
        Point[] pitch = new Point[256];
        Point[] yaw = new Point[256];
        
        // Lire le fichier et le parser
        int i = 0;
        do {
            string textLine = file.ReadLine();
            float[] values = Array.ConvertAll<string, float>(textLine.Split(';'), new Converter<string, float>(StringToFloat));
            long time = (long)(values[0] * 1000); // values[0] est le temps en secondes
            height[i] = new Point(i, values[1], 0);
            roll[i] = new Point(i, values[2], 0);
            pitch[i] = new Point(i, values[3], 0);
            yaw[i] = new Point(i, values[4], 0);
            i++;
        } while (file.Peek() != -1);
        
        // Ecrire les fichiers de figures
        GestureIO.WriteGesture(height, figure, filePath + figure + "_" + "height" + ".xml");
        GestureIO.WriteGesture(roll, figure, filePath + figure + "_" + "roll" + ".xml");
        GestureIO.WriteGesture(pitch, figure, filePath + figure + "_" + "pitch" + ".xml");
        GestureIO.WriteGesture(yaw, figure, filePath + figure + "_" + "yaw" + ".xml");
    }


    private void CSVParser(StreamReader file, Point[] height, Point[] roll, Point[] pitch, Point[] yaw) {
        // Lire le fichier et le parser
        int i = 0;
        do {
            string textLine = file.ReadLine();
            float[] values = Array.ConvertAll<string, float>(textLine.Split(';'), new Converter<string, float>(StringToFloat));
            long time = (long)(values[0] * 1000); // values[0] est le temps en secondes
            height[i] = new Point(i, values[1], 0);
            roll[i] = new Point(i, values[2], 0);
            pitch[i] = new Point(i, values[3], 0);
            yaw[i] = new Point(i, values[4], 0);
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
        Point[] height = new Point[256];
        Point[] roll = new Point[256];
        Point[] pitch = new Point[256];
        Point[] yaw = new Point[256];

        // Lire le fichier et le parser
        CSVParser(file, height, roll, pitch, yaw);

        // Créer les figures
        gesturesHeight[next[0] ++]= new Gesture(height, "Bosse");
        gesturesRoll[next[1]++] = new Gesture(roll, "LigneCoupee");
        gesturesPitch[next[2]] = new Gesture(pitch, "ZigZag");
        gesturesYaw[next[3]] = new Gesture(yaw, "LigneCoupee");
    }


    private void Roll() {
        StreamReader file = new StreamReader(filePath + "Roll" + "-Traj.csv");
        Point[] height = new Point[256];
        Point[] roll = new Point[256];
        Point[] pitch = new Point[256];
        Point[] yaw = new Point[256];

        // Lire le fichier et le parser
        CSVParser(file, height, roll, pitch, yaw);

        // Créer les figures
        gesturesHeight[next[0]++] = new Gesture(height, "LigneDroite");
        gesturesRoll[next[1]++] = new Gesture(roll, "LigneMontante");
        gesturesPitch[next[2]] = new Gesture(pitch, "LigneDroite");
        gesturesYaw[next[3]] = new Gesture(yaw, "LigneDroite");
    }


    private void CubanEight() {
        StreamReader file = new StreamReader(filePath + "CubanEight" + "-Traj.csv");
        Point[] height = new Point[256];
        Point[] roll = new Point[256];
        Point[] pitch = new Point[256];
        Point[] yaw = new Point[256];

        // Lire le fichier et le parser
        CSVParser(file, height, roll, pitch, yaw);

        // Créer les figures
        gesturesHeight[next[0]++] = new Gesture(height, "DoubleBosse");
        gesturesRoll[next[1]++] = new Gesture(roll, "DoubleDemieLigneMontante");
        gesturesPitch[next[2]] = new Gesture(pitch, "DoubleZigZag");
        gesturesYaw[next[3]] = new Gesture(yaw, "LigneCoupee");
    }

    #endregion
}

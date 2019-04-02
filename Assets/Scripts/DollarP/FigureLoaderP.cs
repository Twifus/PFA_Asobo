using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using PDollarGestureRecognizer;
using System.Globalization;
using UnityEngine;

public class FigureLoaderP
{

    #region Members

    private readonly string filePath = "./Assets/Figures/";
    private string[] figures = { "Roll", "Loop" };
    private string[] curves = { "height", "roll", "pitch", "yaw" };

    #endregion

    #region Loader

    public void LoadFigures(List<Gesture> height, List<Gesture> roll, List<Gesture> pitch, List<Gesture> yaw)
    {
        Loop(height, roll, pitch, yaw);
        Roll(height, roll, pitch, yaw);
        //CubanEight(height, roll, pitch, yaw);
        StraightLine(height, roll, pitch, yaw);
    }

    #endregion

    #region File Parsing

    // Créé tous les fichiers xml associés à la figure donnée
    private void CreateFigureFiles(string figure)
    {
        StreamReader file = new StreamReader(filePath + figure + ".csv");
        List<Point> height = new List<Point>();
        List<Point> roll = new List<Point>();
        List<Point> pitch = new List<Point>();
        List<Point> yaw = new List<Point>();

        // Lire le fichier et le parser
        file.ReadLine(); // first line doesn't have values
        int i = 0;
        do
        {
            string textLine = file.ReadLine();
            float[] values = Array.ConvertAll<string, float>(textLine.Split(';'), new Converter<string, float>(StringToFloat));
            long time = (long)(values[0] * 1000); // values[0] est le temps en secondes
            height.Add(new Point(i, values[2], 0));
            roll.Add(new Point(i, values[19], 0));
            pitch.Add(new Point(i, values[20], 0));
            yaw.Add(new Point(i, values[21], 0));
            i++;
        } while (file.Peek() != -1);

        // Ecrire les fichiers de figures
        GestureIO.WriteGesture(height.ToArray(), figure, filePath + figure + "_" + "height" + ".xml");
        GestureIO.WriteGesture(roll.ToArray(), figure, filePath + figure + "_" + "roll" + ".xml");
        GestureIO.WriteGesture(pitch.ToArray(), figure, filePath + figure + "_" + "pitch" + ".xml");
        GestureIO.WriteGesture(yaw.ToArray(), figure, filePath + figure + "_" + "yaw" + ".xml");
    }


    private void CSVParser(StreamReader file, List<Point> height, List<Point> roll, List<Point> pitch, List<Point> yaw)
    {
        // Lire le fichier et le parser
        file.ReadLine(); // la première ligne contient les noms de colonnes
        int i = 0;
        do
        {
            string textLine = file.ReadLine();
            float[] values = Array.ConvertAll<string, float>(textLine.Split(';'), new Converter<string, float>(StringToFloat));
            long time = (long)(values[0] * 1000); // values[0] est le temps en secondes
            height.Add(new Point(i, values[2], 0));
            roll.Add(new Point(i, values[11], 0)); // plane.right
            pitch.Add(new Point(i, values[14], 0)); // plane.up
            yaw.Add(new Point(i, values[17], 0)); // plane.forward
            i++;
        } while (file.Peek() != -1);
    }

    // Convertisseur de string (au format "xx.xxx") en float
    private float StringToFloat(string s)
    {
        return float.Parse(s, CultureInfo.InvariantCulture);
    }

    #endregion

    #region Figures

    private void Loop(List<Gesture> gesturesHeight, List<Gesture> gesturesRoll, List<Gesture> gesturesPitch, List<Gesture> gesturesYaw)
    {
        StreamReader file = new StreamReader(filePath + "Perfect-Loop" + ".csv");
        List<Point> height = new List<Point>();
        List<Point> roll = new List<Point>();
        List<Point> pitch = new List<Point>();
        List<Point> yaw = new List<Point>();

        // Lire le fichier et le parser
        CSVParser(file, height, roll, pitch, yaw);

        // Créer les figures
        gesturesHeight.Add(new Gesture(height.ToArray(), DollarFigure.loop[0]));
        gesturesRoll.Add(new Gesture(roll.ToArray(), DollarFigure.loop[1]));
        gesturesPitch.Add(new Gesture(pitch.ToArray(), DollarFigure.loop[2]));
        gesturesYaw.Add(new Gesture(yaw.ToArray(), DollarFigure.loop[3]));
        file.Close();
    }


    private void Roll(List<Gesture> gesturesHeight, List<Gesture> gesturesRoll, List<Gesture> gesturesPitch, List<Gesture> gesturesYaw)
    {
        StreamReader file = new StreamReader(filePath + "Perfect-Barrel-L" + ".csv");
        List<Point> height = new List<Point>();
        List<Point> roll = new List<Point>();
        List<Point> pitch = new List<Point>();
        List<Point> yaw = new List<Point>();

        // Lire le fichier et le parser
        CSVParser(file, height, roll, pitch, yaw);

        // Créer les figures
        gesturesHeight.Add(new Gesture(height.ToArray(), DollarFigure.barrelL[0]));
        gesturesRoll.Add(new Gesture(roll.ToArray(), DollarFigure.barrelL[1]));
        gesturesPitch.Add(new Gesture(pitch.ToArray(), DollarFigure.barrelL[2]));
        gesturesYaw.Add(new Gesture(yaw.ToArray(), DollarFigure.barrelL[3]));
        file.Close();

        file = new StreamReader(filePath + "Perfect-Barrel-R" + ".csv");
        height = new List<Point>();
        roll = new List<Point>();
        pitch = new List<Point>();
        yaw = new List<Point>();

        // Lire le fichier et le parser
        CSVParser(file, height, roll, pitch, yaw);

        // Créer les figures
        gesturesHeight.Add(new Gesture(height.ToArray(), DollarFigure.barrelR[0]));
        gesturesRoll.Add(new Gesture(roll.ToArray(), DollarFigure.barrelR[1]));
        gesturesPitch.Add(new Gesture(pitch.ToArray(), DollarFigure.barrelR[2]));
        gesturesYaw.Add(new Gesture(yaw.ToArray(), DollarFigure.barrelR[3]));
        file.Close();
    }


    private void CubanEight(List<Gesture> gesturesHeight, List<Gesture> gesturesRoll, List<Gesture> gesturesPitch, List<Gesture> gesturesYaw)
    {
        StreamReader file = new StreamReader(filePath + "CubanEight" + "-Traj.csv");
        List<Point> height = new List<Point>();
        List<Point> roll = new List<Point>();
        List<Point> pitch = new List<Point>();
        List<Point> yaw = new List<Point>();

        // Lire le fichier et le parser
        CSVParser(file, height, roll, pitch, yaw);

        // Créer les figures
        gesturesHeight.Add(new Gesture(height.ToArray(), DollarFigure.cubanEight[0]));
        gesturesRoll.Add(new Gesture(roll.ToArray(), DollarFigure.cubanEight[1]));
        gesturesPitch.Add(new Gesture(pitch.ToArray(), DollarFigure.cubanEight[2]));
        gesturesYaw.Add(new Gesture(yaw.ToArray(), DollarFigure.cubanEight[3]));
        file.Close();
    }

    private void StraightLine(List<Gesture> gesturesHeight, List<Gesture> gesturesRoll, List<Gesture> gesturesPitch, List<Gesture> gesturesYaw)
    {
        StreamReader file = new StreamReader(filePath + "Perfect-StraightLine" + ".csv");
        List<Point> height = new List<Point>();
        List<Point> roll = new List<Point>();
        List<Point> pitch = new List<Point>();
        List<Point> yaw = new List<Point>();

        // Lire le fichier et le parser
        CSVParser(file, height, roll, pitch, yaw);

        // Créer les figures
        gesturesHeight.Add(new Gesture(height.ToArray(), DollarFigure.straightLine[0]));
        gesturesRoll.Add(new Gesture(roll.ToArray(), DollarFigure.straightLine[1]));
        gesturesPitch.Add(new Gesture(pitch.ToArray(), DollarFigure.straightLine[2]));
        gesturesYaw.Add(new Gesture(yaw.ToArray(), DollarFigure.straightLine[3]));
        file.Close();
    }

    #endregion
}

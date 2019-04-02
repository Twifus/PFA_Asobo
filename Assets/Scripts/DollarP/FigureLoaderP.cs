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

    private readonly List<Gesture> altitude;
    private readonly List<Gesture> rightScalar;
    private readonly List<Gesture> upScalar;
    private readonly List<Gesture> forwardScalar;

    #endregion

    #region Constructor

    public FigureLoaderP(List<Gesture> height, List<Gesture> roll, List<Gesture> pitch, List<Gesture> yaw) {
        this.altitude = height;
        this.rightScalar = roll;
        this.upScalar = pitch;
        this.forwardScalar = yaw;
    }

    #endregion

    #region Loader

    public void LoadFigures() {
        // Clear previous gestures
        altitude.Clear();
        rightScalar.Clear();
        upScalar.Clear();
        forwardScalar.Clear();

        // Load new gestures
        Load(filePath + "Perfect-Loop.csv", DollarFigure.loop);
        Load(filePath + "Perfect-Barrel-R.csv", DollarFigure.barrelR);
        Load(filePath + "Perfect-Barrel-L.csv", DollarFigure.barrelL);
        //Load(filePath + "CubanEight-Traj.csv", DollarFigure.cubanEight); // file need to be update
        Load(filePath + "Perfect-StraightLine.csv", DollarFigure.straightLine);
    }

    private void Load(string file, string[] curvesName) {
        StreamReader streamReader = new StreamReader(file);
        List<Point> height = new List<Point>();
        List<Point> roll = new List<Point>();
        List<Point> pitch = new List<Point>();
        List<Point> yaw = new List<Point>();

        // Read file and parse it
        CSVParser(streamReader, height, roll, pitch, yaw);

        // Create gestures
        altitude.Add(new Gesture(height.ToArray(), curvesName[0]));
        rightScalar.Add(new Gesture(roll.ToArray(), curvesName[1]));
        upScalar.Add(new Gesture(pitch.ToArray(), curvesName[2]));
        forwardScalar.Add(new Gesture(yaw.ToArray(), curvesName[3]));

        streamReader.Close();
    }

    #endregion

    #region File Parsing

    private void CSVParser(StreamReader file, List<Point> height, List<Point> roll, List<Point> pitch, List<Point> yaw)
    {
        // Read file and parse it
        file.ReadLine(); // first line is column's names
        int i = 0;
        do
        {
            string textLine = file.ReadLine();
            float[] values = Array.ConvertAll<string, float>(textLine.Split(';'), new Converter<string, float>(StringToFloat));
            long time = (long)(values[0] * 1000); // values[0] is time in second, and time must be in ms
            height.Add(new Point(i, values[2], 0));
            roll.Add(new Point(i, values[11], 0)); // plane.right
            pitch.Add(new Point(i, values[14], 0)); // plane.up
            yaw.Add(new Point(i, values[17], 0)); // plane.forward
            i++;
        } while (file.Peek() != -1);
    }

    // Convert string (from format "xx.xxx") to float
    private float StringToFloat(string s)
    {
        return float.Parse(s, CultureInfo.InvariantCulture);
    }

    #endregion
}

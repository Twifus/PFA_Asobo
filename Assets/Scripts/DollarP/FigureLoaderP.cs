using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using PDollarGestureRecognizer;
using System.Globalization;

public class FigureLoaderP
{
    #region Members

    private readonly List<Gesture>[] gestures;

    #endregion

    #region Constructor

    public FigureLoaderP(List<Gesture>[] gestures) {
        this.gestures = gestures;
    }

    #endregion

    #region Loader

    public void LoadFigures() {
        // Clear previous gestures
        for (int i = 0; i < gestures.Length; i++) {
            gestures[i].Clear();
        }

        // Load new gestures
        Load(DollarFigure.filePath + "Perfect-Loop.csv", DollarFigure.loop);
        Load(DollarFigure.filePath + "Perfect-Barrel-R.csv", DollarFigure.barrelR);
        Load(DollarFigure.filePath + "Perfect-Barrel-L.csv", DollarFigure.barrelL);
        Load(DollarFigure.filePath + "Perfect-CubanEight.csv", DollarFigure.cubanEight);
        Load(DollarFigure.filePath + "Perfect-StraightLine.csv", DollarFigure.straightLine);
    }

    private void Load(string file, string[] curvesNames) {
        StreamReader streamReader = new StreamReader(file);
        List<Point>[] points = new List<Point>[gestures.Length];
        for (int i = 0; i < points.Length; i++) {
            points[i] = new List<Point>();
        }

        // Read file and parse it
        CSVParser(streamReader, points);

        // Create gestures
        for (int i = 0; i < gestures.Length; i++) {
            gestures[i].Add(new Gesture(points[i].ToArray(), curvesNames[i]));
        }

        streamReader.Close();
    }

    #endregion

    #region File Parsing
    
    private void CSVParser(StreamReader file, List<Point>[] points) {
        // Read file and parse it
        file.ReadLine(); // first line is column's names
        int i = 0;
        do {
            string textLine = file.ReadLine();
            float[] values = Array.ConvertAll<string, float>(textLine.Split(';'), new Converter<string, float>(StringToFloat));
            for (int j = 0; j < points.Length; j++) {
                points[j].Add(new Point(i, values[ DollarFigure.columnToPick[j] ], 0));
            }
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

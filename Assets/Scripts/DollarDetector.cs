using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
//using U = UnityEngine;
using System.IO;
using PDollarGestureRecognizer;

public class DollarDetector : IFigureDetection {

    #region Members

    private readonly int MAX_SIZE = 280;
    private readonly int MINPOINTSCOUNT = 10;
    private int time = 0;

    private StreamWriter Writer;

    protected List<Point>[] _timePoints;
    protected List<Gesture>[] gestures;

    #endregion

    #region Constructor

    public DollarDetector()
    {
        gestures = new List<Gesture>[DollarFigure.curvesPerFigure];
        for (int i = 0; i < gestures.Length; i++) {
            gestures[i] = new List<Gesture>();
        }
        
        FigureLoaderP loader = new FigureLoaderP(gestures);
        loader.LoadFigures();
        
        _timePoints = new List<Point>[DollarFigure.curvesPerFigure];
        for (int i = 0; i < _timePoints.Length; i++) {
            _timePoints[i] = new List<Point>();
        }
    }

    #endregion

    #region Set Points

    // Add given coordinate to lists
    public void setPoint(Coordinate point) {
        if (_timePoints[0].Count == MAX_SIZE)
        {
            // Remove first element
            for (int j = 0; j < _timePoints.Length; j++) {
                _timePoints[j].RemoveAt(0);
            }

            // Update X coordinates
            for (int i = 0; i < MAX_SIZE - 1; i++)
            {
                for (int j = 0; j < _timePoints.Length; j++) {
                    _timePoints[j][i].X--;
                }
            }
        }
        else
        {
            time++;
        }

        // Add new points
        _timePoints[0].Add(new Point(time, point.ypos, 0));
        _timePoints[1].Add(new Point(time, point.roll, 0));
        _timePoints[2].Add(new Point(time, point.pitch, 0));
        _timePoints[3].Add(new Point(time, point.yaw, 0));
    }

    // Add current flying object coordinate to lists
    public void setPoint(IFlyingObject flyingObject) {
        if (_timePoints[0].Count == MAX_SIZE)
        {
            // Remove first element
            for (int j = 0; j < _timePoints.Length; j++) {
                _timePoints[j].RemoveAt(0);
            }

            // Update X coordinates
            for (int i = 0; i < MAX_SIZE - 1; i++) {
                for (int j = 0; j < _timePoints.Length; j++) {
                    _timePoints[j][i].X--;
                }
            }
        }
        else
        {
            time++;
        }

        float rightScalar = Vector3.Dot(flyingObject.right, Vector3.UnitY);
        float upScalar = Vector3.Dot(flyingObject.up, Vector3.UnitY);
        float forwardScalar = Vector3.Dot(flyingObject.forward, Vector3.UnitY);

        // Add new points
        _timePoints[0].Add(new Point(time, flyingObject.pos.Y, 0));
        _timePoints[1].Add(new Point(time, rightScalar, 0));
        _timePoints[2].Add(new Point(time, upScalar, 0));
        _timePoints[3].Add(new Point(time, forwardScalar, 0));
    }

    #endregion

    #region Detection

    /// <summary>
    /// Check if best results match the curves names given
    /// </summary>
    /// <param name="results"></param>
    /// <param name="curvesNames"></param>
    /// <returns></returns>
    protected bool AnalyseResults(BestGesture[] results, string[] curvesNames) {
        bool ret = true;
        for (int i = 0; i < results.Length; i++) {
            ret = ret && results[i].Name.Equals(curvesNames[i]) /*&& results[i].Score > 0.8f*/;
        }
        return ret;
    }

    /// <summary>
    /// Return a list of figures done, based on the best results
    /// </summary>
    /// <returns></returns>
    public List<Figure> detection() {
        //return DetectionBis();
        List<Figure> figures = new List<Figure>();

        //U.Debug.Log(_timePoints[0].Count);

        if (_timePoints[0].Count > MINPOINTSCOUNT)
        {
            // Get best gestures for each curve
            BestGesture[] results = new BestGesture[DollarFigure.curvesPerFigure];
            for (int i = 0; i < results.Length; i++) {
                results[i] = PointCloudRecognizer.Classify(new Gesture(_timePoints[i].ToArray()), gestures[i].ToArray());
            }

            // Analyse results

            // Straight Line
            if (AnalyseResults(results, DollarFigure.straightLine)) { // buffer cleaner
                //U.Debug.Log("StraightLine");
                //U.Debug.Log(resultHeight.Score + ", " + resultRoll.Score + ", " + resultPitch.Score + ", " + resultYaw.Score);
                ClearLists();
            }

            // Loop
            if (AnalyseResults(results, DollarFigure.loop)) {
                //U.Debug.Log("Loop");
                //U.Debug.Log(resultHeight.Score + ", " + resultRoll.Score + ", " + resultPitch.Score + ", " + resultYaw.Score);
                figures.Add(new Figure(figure_id.LOOP, 1f));
                ClearLists();
            }

            // Roll
            if (AnalyseResults(results, DollarFigure.barrelL) || AnalyseResults(results, DollarFigure.barrelR)) {
                //U.Debug.Log("Roll");
                //U.Debug.Log(resultHeight.Score + ", " + resultRoll.Score + ", " + resultPitch.Score + ", " + resultYaw.Score);
                figures.Add(new Figure(figure_id.BARREL, 1f));
                ClearLists();
            }

            // Cuban Eight
            if (AnalyseResults(results, DollarFigure.cubanEight)) {
                //U.Debug.Log("CubanEight");
                figures.Add(new Figure(figure_id.CUBANEIGHT, 1f));
                ClearLists();
            }
        }
        return figures;
	}


    /// <summary>
    /// Calculte the average score of a figure based on the results and curves names
    /// </summary>
    /// <param name="results"></param>
    /// <param name="curvesNames"></param>
    /// <returns></returns>
    private float FigureScore(Dictionary<string, double>[] results, string[] curvesNames) {
        double moy = 0;
        for (int i = 0; i < results.Length; i++) {
            moy += results[i].ContainsKey(curvesNames[i]) ? results[i][curvesNames[i]] : 0; // add key score or 0 if key does not exist
        }
        return (float)moy / 4;
    }

    /// <summary>
    /// Analyse results and return the figure best recognize based on the average score
    /// </summary>
    /// <param name="results"></param>
    /// <returns></returns>
    private Figure FigureDone(Dictionary<string, double>[] results) {
        float straightLine = FigureScore(results, DollarFigure.straightLine);
        float loop = FigureScore(results, DollarFigure.loop);
        float barrel = FigureScore(results, DollarFigure.barrelL);
        float cubanEight = FigureScore(results, DollarFigure.cubanEight);
        float max = Math.Max(Math.Max(straightLine, loop), Math.Max(barrel, cubanEight));
        //U.Debug.Log("scores : loop = " + loop + "; barrel = " + barrel + "; line = " + straightLine);
        
        // Return a figure depending on the max
        if (max == loop && loop > 0.6) {
            ClearLists();
            return new Figure(figure_id.LOOP, 1);
        }
        else if (max == barrel && barrel > 0.85) {
            ClearLists();
            return new Figure(figure_id.BARREL, 1);
        }
        else if (max == cubanEight && cubanEight > 0.8) {
            ClearLists();
            return new Figure(figure_id.CUBANEIGHT, 1);
        }
        else if (straightLine > 0.9) {
            ClearLists();
            return null;
        }
        return null;
    }

    /// <summary>
    /// Return a list of figures done, based on average score on each figure
    /// </summary>
    /// <returns></returns>
    public List<Figure> DetectionBis() {
        List<Figure> result = new List<Figure>();

        //U.Debug.Log(_timePoints[0].Count);

        if (_timePoints[0].Count > MINPOINTSCOUNT) {
            // Get scores for each gesture for each curve
            Dictionary<string, double>[] results = new Dictionary<string, double>[DollarFigure.curvesPerFigure];
            for (int i = 0; i < results.Length; i++) {
                results[i] = PointCloudRecognizer.Recognize(new Gesture(_timePoints[i].ToArray()), gestures[i].ToArray());
            }

            // Analyse results
            Figure figure = FigureDone(results);
            if (figure != null) {
                //U.Debug.Log(figure.id);
                result.Add(figure);
            }
        }
        return result;
    }

    #endregion

    #region Utility Methods

    // Remove content of all lists
    protected void ClearLists() {
        //U.Debug.Log("Clear");
        for (int i = 0; i < _timePoints.Length; i++) {
            _timePoints[i].Clear();
        }
        time = 0;
    }

    // DEBUG - Save current list in textfile
    public void WriteLists() {
        string path = string.Format("../DollarLog-{0}", System.DateTime.Now.ToFileTime());
        Writer = new StreamWriter(path + ".csv", true);
        for (int i = 0; i < _timePoints[0].Count; i++) {
            Writer.WriteLine(string.Format("{0};{1};{2};{3};{4}",
            _timePoints[0][i].X, _timePoints[0][i].Y, _timePoints[1][i].Y, _timePoints[2][i].Y, _timePoints[3][i].Y));
        }
        Writer.Close();
    }

    #endregion
}

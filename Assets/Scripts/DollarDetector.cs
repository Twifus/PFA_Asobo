using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using U = UnityEngine;
using System.IO;
using PDollarGestureRecognizer;

public class DollarDetector : IFigureDetection {

    private int MAX_SIZE = 280;
    private int time = 0;

    private StreamWriter Writer;

    protected List<Point> _timePointsHeight;
    protected List<Point> _timePointsRoll;
    protected List<Point> _timePointsPitch;
    protected List<Point> _timePointsYaw;

    protected List<Gesture> gesturesHeight;
    protected List<Gesture> gesturesRoll;
    protected List<Gesture> gesturesPitch;
    protected List<Gesture> gesturesYaw;


    public DollarDetector()
    {
        gesturesHeight = new List<Gesture>();
        gesturesRoll = new List<Gesture>();
        gesturesPitch = new List<Gesture>();
        gesturesYaw = new List<Gesture>();
        FigureLoaderP loader = new FigureLoaderP();
        loader.LoadFigures(gesturesHeight, gesturesRoll, gesturesPitch, gesturesYaw);

        _timePointsHeight = new List<Point>();
        _timePointsRoll = new List<Point>();
        _timePointsPitch = new List<Point>();
        _timePointsYaw = new List<Point>();
    }

    // Add given coordinate to lists
    public void setPoint(Coordinate point) {
        if (_timePointsHeight.Count == MAX_SIZE)
        {
            // Remove first element
            _timePointsHeight.RemoveAt(0);
            _timePointsRoll.RemoveAt(0);
            _timePointsPitch.RemoveAt(0);
            _timePointsYaw.RemoveAt(0);

            // Update X coordinates
            int i;
            for (i = 0; i < MAX_SIZE - 1; i++)
            {
                _timePointsHeight[i].X--;
                _timePointsRoll[i].X--;
                _timePointsPitch[i].X--;
                _timePointsYaw[i].X--;
            }
        }
        else
        {
            time++;
        }

        _timePointsHeight.Add(new Point(time, point.ypos, 0));
        _timePointsRoll.Add(new Point(time, point.roll, 0));
        _timePointsPitch.Add(new Point(time, point.pitch, 0));
        _timePointsYaw.Add(new Point(time, point.yaw, 0));
    }

    // Add current flying object coordinate to lists
    public void setPoint(IFlyingObject flyingObject) {
        if (_timePointsHeight.Count == MAX_SIZE)
        {
            // Remove first element
            _timePointsHeight.RemoveAt(0);
            _timePointsRoll.RemoveAt(0);
            _timePointsPitch.RemoveAt(0);
            _timePointsYaw.RemoveAt(0);

            // Update X coordinates
            int i;
            for (i = 0; i < MAX_SIZE - 1; i++)
            {
                _timePointsHeight[i].X--;
                _timePointsRoll[i].X--;
                _timePointsPitch[i].X--;
                _timePointsYaw[i].X--;
            }
        }
        else
        {
            time++;
        }

        float rightScalar = Vector3.Dot(flyingObject.right, Vector3.UnitY);
        float upScalar = Vector3.Dot(flyingObject.up, Vector3.UnitY);
        float forwardScalar = Vector3.Dot(flyingObject.forward, Vector3.UnitY);

        _timePointsHeight.Add(new Point(time, flyingObject.pos.Y, 0));
        //_timePointsRoll.Add(new Point(time, flyingObject.rollScalar, 0));
        //_timePointsPitch.Add(new Point(time, flyingObject.pitchScalar, 0));
        //_timePointsYaw.Add(new Point(time, flyingObject.yawScalar, 0));
        _timePointsRoll.Add(new Point(time, rightScalar, 0));
        _timePointsPitch.Add(new Point(time, upScalar, 0));
        _timePointsYaw.Add(new Point(time, forwardScalar, 0));
    }

    // Remove content of all lists
    protected void ClearLists()
    {
        //U.Debug.Log("Clear");
        _timePointsHeight.Clear();
        _timePointsRoll.Clear();
        _timePointsPitch.Clear();
        _timePointsYaw.Clear();
        time = 0;
    }

    // DEBUG - Save current list in textfile
    public void WriteLists()
    {
        string path = string.Format("../DollarLog-{0}", System.DateTime.Now.ToFileTime());
        Writer = new StreamWriter(path + ".csv", true);
        for (int i = 0; i < _timePointsHeight.Count; i++)
        {
            Writer.WriteLine(string.Format("{0};{1};{2};{3};{4}",
            _timePointsHeight[i].X, _timePointsHeight[i].Y, _timePointsRoll[i].Y, _timePointsPitch[i].Y, _timePointsYaw[i].Y));
        }
        Writer.Close();
    }

    protected bool AnalyseResults(BestGesture resultHeight, BestGesture resultRoll, BestGesture resultPitch, BestGesture resultYaw, string height, string roll, string pitch, string yaw)
    {
        return (resultHeight.Name.Equals(height) //&& resultHeight.Score > 0.8f
            && resultRoll.Name.Equals(roll) //&& resultRoll.Score > 0.8f
            && resultPitch.Name.Equals(pitch) //&& resultPitch.Score > 0.8f
            && resultYaw.Name.Equals(yaw)); //&& resultYaw.Score > 0.8f);
    }

    private float FigureScore(Dictionary<string, double> resultHeight, Dictionary<string, double> resultRoll, Dictionary<string, double> resultPitch, Dictionary<string, double> resultYaw, string height, string roll, string pitch, string yaw) {
        double moy = 0;
        moy += resultHeight.ContainsKey(height) ? resultHeight[height] : 0;
        moy += resultRoll.ContainsKey(roll) ? resultRoll[roll] : 0;
        moy += resultPitch.ContainsKey(pitch) ? resultPitch[pitch] : 0;
        moy += resultYaw.ContainsKey(yaw) ? resultYaw[yaw] : 0;
        return (float)moy / 4;
    }

    private Figure FigureDone(Dictionary<string, double> resultHeight, Dictionary<string, double> resultRoll, Dictionary<string, double> resultPitch, Dictionary<string, double> resultYaw) {
        float straightLine = FigureScore(resultHeight, resultRoll, resultPitch, resultYaw, DollarFigure.straightLine[0], DollarFigure.straightLine[1], DollarFigure.straightLine[2], DollarFigure.straightLine[3]);
        float loop = FigureScore(resultHeight, resultRoll, resultPitch, resultYaw, DollarFigure.loop[0], DollarFigure.loop[1], DollarFigure.loop[2], DollarFigure.loop[3]);
        float barrel = FigureScore(resultHeight, resultRoll, resultPitch, resultYaw, DollarFigure.barrelL[0], DollarFigure.barrelL[1], DollarFigure.barrelL[2], DollarFigure.barrelL[3]);
        float cubanEight = FigureScore(resultHeight, resultRoll, resultPitch, resultYaw, DollarFigure.cubanEight[0], DollarFigure.cubanEight[1], DollarFigure.cubanEight[2], DollarFigure.cubanEight[3]);
        float max = Math.Max(Math.Max(straightLine, loop), Math.Max(barrel, cubanEight));
        //U.Debug.Log("scores : loop = " + loop + "; barrel = " + barrel + "; line = " + straightLine);
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

    public List<Figure> DetectionBis() {
        List<Figure> result = new List<Figure>();

        //U.Debug.Log(_timePointsHeight.Count);

        if (_timePointsHeight.Count > 60) {
            Dictionary<string, double> resultHeight = PointCloudRecognizer.Recognize(new Gesture(_timePointsHeight.ToArray(), "test height"), gesturesHeight.ToArray());
            Dictionary<string, double> resultRoll = PointCloudRecognizer.Recognize(new Gesture(_timePointsRoll.ToArray(), "test roll"), gesturesRoll.ToArray());
            Dictionary<string, double> resultPitch = PointCloudRecognizer.Recognize(new Gesture(_timePointsPitch.ToArray(), "test pitch"), gesturesPitch.ToArray());
            Dictionary<string, double> resultYaw = PointCloudRecognizer.Recognize(new Gesture(_timePointsYaw.ToArray(), "test yax"), gesturesYaw.ToArray());

            //U.Debug.Log(resultHeight.Name + ", " + resultRoll.Name + ", " + resultPitch.Name + ", " + resultYaw.Name);

            Figure figure = FigureDone(resultHeight, resultRoll, resultPitch, resultYaw);
            if (figure != null) {
                //U.Debug.Log(figure.id);
                result.Add(figure);
            }
        }
        return result;
    }

    public List<Figure> detection() {
        //return DetectionBis();
        List<Figure> result = new List<Figure>();

        //U.Debug.Log(_timePointsHeight.Count);

        if (_timePointsHeight.Count > 10)
        {
            BestGesture resultHeight = PointCloudRecognizer.Classify(new Gesture(_timePointsHeight.ToArray(), "test height"), gesturesHeight.ToArray());
            BestGesture resultRoll = PointCloudRecognizer.Classify(new Gesture(_timePointsRoll.ToArray(), "test roll"), gesturesRoll.ToArray());
            BestGesture resultPitch = PointCloudRecognizer.Classify(new Gesture(_timePointsPitch.ToArray(), "test pitch"), gesturesPitch.ToArray());
            BestGesture resultYaw = PointCloudRecognizer.Classify(new Gesture(_timePointsYaw.ToArray(), "test yax"), gesturesYaw.ToArray());

            U.Debug.Log(resultHeight.Name + ", " + resultRoll.Name + ", " + resultPitch.Name + ", " + resultYaw.Name);

            // Straight Line
            if (AnalyseResults(resultHeight, resultRoll, resultPitch, resultYaw, DollarFigure.straightLine[0], DollarFigure.straightLine[1], DollarFigure.straightLine[2], DollarFigure.straightLine[3])) {
                //U.Debug.Log("StraightLine");
                //U.Debug.Log(resultHeight.Score + ", " + resultRoll.Score + ", " + resultPitch.Score + ", " + resultYaw.Score);
                ClearLists();
            }

            // Loop
            if (AnalyseResults(resultHeight, resultRoll, resultPitch, resultYaw, DollarFigure.loop[0], DollarFigure.loop[1], DollarFigure.loop[2], DollarFigure.loop[3]))
            {
                //U.Debug.Log("Loop");
                //U.Debug.Log(resultHeight.Score + ", " + resultRoll.Score + ", " + resultPitch.Score + ", " + resultYaw.Score);
                result.Add(new Figure());
                result[0].id = figure_id.LOOP;
                result[0].quality = 1f;
                ClearLists();
            }

            // Roll
            if (AnalyseResults(resultHeight, resultRoll, resultPitch, resultYaw, DollarFigure.barrelL[0], DollarFigure.barrelL[1], DollarFigure.barrelL[2], DollarFigure.barrelL[3]) 
                || AnalyseResults(resultHeight, resultRoll, resultPitch, resultYaw, DollarFigure.barrelR[0], DollarFigure.barrelR[1], DollarFigure.barrelR[2], DollarFigure.barrelR[3]))
            {
                
                //U.Debug.Log("Roll");
                //U.Debug.Log(resultHeight.Score + ", " + resultRoll.Score + ", " + resultPitch.Score + ", " + resultYaw.Score);
                result.Add(new Figure());
                result[0].id = figure_id.BARREL;
                result[0].quality = 1f;
                ClearLists();
            }

            // Cuban Eight
            if (AnalyseResults(resultHeight, resultRoll, resultPitch, resultYaw, DollarFigure.cubanEight[0], DollarFigure.cubanEight[1], DollarFigure.cubanEight[2], DollarFigure.cubanEight[3]))
            {
                //U.Debug.Log("CubanEight");
                result.Add(new Figure());
                result[0].id = figure_id.CUBANEIGHT;
                result[0].quality = 1f;
                ClearLists();
            }
        }
        return result;
	}
}

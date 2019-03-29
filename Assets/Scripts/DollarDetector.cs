using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public void setPoint(Coordinate point) {
        if (_timePointsHeight.Count == MAX_SIZE)
        {
            _timePointsHeight.RemoveAt(0);
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

    public void setPoint(IFlyingObject flyingObject) {
        if (_timePointsHeight.Count == MAX_SIZE) {
            _timePointsHeight.RemoveAt(0);
            int i;
            for (i = 0; i < MAX_SIZE - 1; i++) {
                _timePointsHeight[i].X--;
                _timePointsRoll[i].X--;
                _timePointsPitch[i].X--;
                _timePointsYaw[i].X--;
            }
        }
        else {
            time++;
        }

        _timePointsHeight.Add(new Point(time, flyingObject.pos.Y, 0));
        _timePointsRoll.Add(new Point(time, flyingObject.rollScalar*100, 0));
        _timePointsPitch.Add(new Point(time, flyingObject.pitchScalar*100, 0));
        _timePointsYaw.Add(new Point(time, flyingObject.yawScalar*100, 0));
    }

    protected void ClearLists()
    {
        _timePointsHeight.Clear();
        _timePointsRoll.Clear();
        _timePointsPitch.Clear();
        _timePointsYaw.Clear();
        time = 0;
    }

    public void WriteLists()
    {
        string path = string.Format("../Figure-{0}", System.DateTime.Now.ToFileTime());
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

	public List<Figure> detection() {
        List<Figure> result = new List<Figure>();

        //Debug.Log(_timePointsHeight.Count);

        if (_timePointsHeight.Count > 10)
        {
            BestGesture resultHeight = PointCloudRecognizer.Classify(new Gesture(_timePointsHeight.ToArray(), "test height"), gesturesHeight.ToArray());
            BestGesture resultRoll = PointCloudRecognizer.Classify(new Gesture(_timePointsRoll.ToArray(), "test roll"), gesturesRoll.ToArray());
            BestGesture resultPitch = PointCloudRecognizer.Classify(new Gesture(_timePointsPitch.ToArray(), "test pitch"), gesturesPitch.ToArray());
            BestGesture resultYaw = PointCloudRecognizer.Classify(new Gesture(_timePointsYaw.ToArray(), "test yax"), gesturesYaw.ToArray());

            //Debug.Log(resultHeight.Name + ", " + resultRoll.Name + ", " + resultPitch.Name + ", " + resultYaw.Name);

            // Straight Line
            if (AnalyseResults(resultHeight, resultRoll, resultPitch, resultYaw, "LigneDroite", "LigneDroite", "LigneDroite", "LigneDroite")) {
                //Debug.Log("StraightLine");
                //Debug.Log(resultHeight.Score + ", " + resultRoll.Score + ", " + resultPitch.Score + ", " + resultYaw.Score);
                ClearLists();
            }

            // Loop
            if (AnalyseResults(resultHeight, resultRoll, resultPitch, resultYaw, "Bosse", "LigneCoupee", "ZigZag", "LigneCoupee"))
            {
                Debug.Log("Loop");
                Debug.Log(resultHeight.Score + ", " + resultRoll.Score + ", " + resultPitch.Score + ", " + resultYaw.Score);
                result.Add(new Figure());
                result[0].id = figure_id.LOOP;
                result[0].quality = 1f;
                ClearLists();
            }

            // Roll
            if (AnalyseResults(resultHeight, resultRoll, resultPitch, resultYaw, "LigneDroite", "LigneMontante", "LigneBizarre", "LigneDroite"))
            {
                Debug.Log("Roll");
                Debug.Log(resultHeight.Score + ", " + resultRoll.Score + ", " + resultPitch.Score + ", " + resultYaw.Score);
                result.Add(new Figure());
                result[0].id = figure_id.BARREL;
                result[0].quality = 1f;
                ClearLists();
            }

            // Cuban Eight
            if (AnalyseResults(resultHeight, resultRoll, resultPitch, resultYaw, "DoubleBosse", "DoubleDemieLigneMontante", "DoubleZigZag", "LigneCoupee"))
            {
                //Debug.Log("CubanEight");
                result.Add(new Figure());
                result[0].id = figure_id.CUBANEIGHT;
                result[0].quality = 1f;
                ClearLists();
            }
        }
        return result;
	}
}

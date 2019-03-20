using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WobbrockLib;
using WobbrockLib.Extensions;
using PDollarGestureRecognizer;

public class DollarDetector : IFigureDetection {

    private int MAX_SIZE = 120;
    private int time = 0;

    List<Point> _timePointsHeight;
    List<Point> _timePointsRoll;
    List<Point> _timePointsPitch;
    List<Point> _timePointsYaw;

    List<Gesture> gesturesHeight;
    List<Gesture> gesturesRoll;
    List<Gesture> gesturesPitch;
    List<Gesture> gesturesYaw;


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
            int i;
            for (i = 0; i < MAX_SIZE - 1; i++)
            {
                _timePointsHeight[i] = _timePointsHeight[i + 1];
                _timePointsRoll[i] = _timePointsRoll[i + 1];
                _timePointsPitch[i] = _timePointsPitch[i + 1];
                _timePointsYaw[i] = _timePointsYaw[i + 1];

                _timePointsHeight[i].X--;
                _timePointsRoll[i].X--;
                _timePointsPitch[i].X--;
                _timePointsYaw[i].X--;
            }
        }
        time++;
        _timePointsHeight.Add(new Point(time, point.ypos, 0));
        _timePointsRoll.Add(new Point(time, point.roll, 0));
        _timePointsPitch.Add(new Point(time, point.pitch, 0));
        _timePointsYaw.Add(new Point(time, point.yaw, 0));
    }

    private void ClearLists()
    {
        _timePointsHeight.Clear();
        _timePointsRoll.Clear();
        _timePointsPitch.Clear();
        _timePointsYaw.Clear();
        time = 0;
    }

    private bool AnalyseResults(BestGesture resultHeight, BestGesture resultRoll, BestGesture resultPitch, BestGesture resultYaw, string height, string roll, string pitch, string yaw)
    {
        return (resultHeight.Name.Equals(height) && resultHeight.Score > 0.7f
            && resultRoll.Name.Equals(roll) //&& resultRoll.Score > 0.7f
            && resultPitch.Name.Equals(pitch) && resultPitch.Score > 0.7f
            && resultYaw.Name.Equals(yaw) && resultYaw.Score > 0.7f);
    }

	public List<Figure> detection() {
        List<Figure> result = new List<Figure>();
        BestGesture resultHeight = PointCloudRecognizer.Classify(new Gesture(_timePointsHeight.ToArray(), "test height"), gesturesHeight.ToArray());
        BestGesture resultRoll = PointCloudRecognizer.Classify(new Gesture(_timePointsRoll.ToArray(), "test roll"), gesturesRoll.ToArray());
        BestGesture resultPitch = PointCloudRecognizer.Classify(new Gesture(_timePointsPitch.ToArray(), "test pitch"), gesturesPitch.ToArray());
        BestGesture resultYaw = PointCloudRecognizer.Classify(new Gesture(_timePointsYaw.ToArray(), "test yax"), gesturesYaw.ToArray());

        //Debug.Log(resultHeight.Name + ", " + resultRoll.Name + ", " + resultPitch.Name + ", " + resultYaw.Name);


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
        if (AnalyseResults(resultHeight, resultRoll, resultPitch, resultYaw, "LigneDroite", "LigneMontante", "LigneDroite", "LigneDroite"))
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
            Debug.Log("CubanEight");
            result.Add(new Figure());
            result[0].id = figure_id.CUBANEIGHT;
            result[0].quality = 1f;
            ClearLists();
        }
        return result;
	}
}

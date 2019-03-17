using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WobbrockLib;
using WobbrockLib.Extensions;
using PDollarGestureRecognizer;

public class DollarDetector : IFigureDetection {

    private int MAX_SIZE = 200;
    private int next = -1;
    private int time = 0;

    Point[] _timePointsHeight;
    Point[] _timePointsRoll;
    Point[] _timePointsPitch;
    Point[] _timePointsYaw;

    Gesture[] gesturesHeight;
    Gesture[] gesturesRoll;
    Gesture[] gesturesPitch;
    Gesture[] gesturesYaw;


    public DollarDetector()
    {
        gesturesHeight = new Gesture[3];
        gesturesRoll = new Gesture[3];
        gesturesPitch = new Gesture[3];
        gesturesYaw = new Gesture[2];
        FigureLoaderP loader = new FigureLoaderP();
        loader.LoadFigures(gesturesHeight, gesturesRoll, gesturesPitch, gesturesYaw);

        _timePointsHeight = new Point[MAX_SIZE];
        _timePointsRoll = new Point[MAX_SIZE];
        _timePointsPitch = new Point[MAX_SIZE];
        _timePointsYaw = new Point[MAX_SIZE];
    }


    public void setPoint(Coordinate point) {
        if (next == MAX_SIZE - 1)
        {
            int i;
            for (i = 0; i < MAX_SIZE - 2; i++)
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
        else
        {
            next++;
        }
        time++;
        _timePointsHeight[next] = new Point(time, point.ypos, 0);
        _timePointsRoll[next] = new Point(time, point.roll, 0);
        _timePointsPitch[next] = new Point(time, point.pitch, 0);
        _timePointsYaw[next] = new Point(time, point.yaw, 0);
    }

    private void ClearLists()
    {
        next = -1;
        time = 0;
    }

    private bool AnalyseResults(BestGesture resultHeight, BestGesture resultRoll, BestGesture resultPitch, BestGesture resultYaw, string height, string roll, string pitch, string yaw)
    {
        return (resultHeight.Name.Equals(height) //&& resultHeight.Score > 0.7f
            && resultRoll.Name.Equals(roll) //&& resultRoll.Score > 0.7f
            && resultPitch.Name.Equals(pitch) //&& resultPitch.Score > 0.7f
            && resultYaw.Name.Equals(yaw)); //&& resultYaw.Score > 0.7f);
    }

	public List<Figure> detection() {
        List<Figure> result = new List<Figure>();

        if (next == MAX_SIZE - 1)
        {
            BestGesture resultHeight = PointCloudRecognizer.Classify(new Gesture(_timePointsHeight, "test height"), gesturesHeight);
            BestGesture resultRoll = PointCloudRecognizer.Classify(new Gesture(_timePointsRoll, "test roll"), gesturesRoll);
            BestGesture resultPitch = PointCloudRecognizer.Classify(new Gesture(_timePointsPitch, "test pitch"), gesturesPitch);
            BestGesture resultYaw = PointCloudRecognizer.Classify(new Gesture(_timePointsYaw, "test yax"), gesturesYaw);

            //Debug.Log(resultHeight.Name + ", " + resultRoll.Name + ", " + resultPitch.Name + ", " + resultYaw.Name);


            // Loop
            if (AnalyseResults(resultHeight, resultRoll, resultPitch, resultYaw, "Bosse", "LigneCoupee", "ZigZag", "LigneCoupee"))
            {
                Debug.Log("Loop");
                result.Add(new Figure());
                result[0].id = figure_id.LOOP;
                result[0].quality = 1f;
                ClearLists();
            }

            // Roll
            if (AnalyseResults(resultHeight, resultRoll, resultPitch, resultYaw, "LigneDroite", "LigneMontante", "LigneDroite", "LigneDroite"))
            {
                Debug.Log("Roll");
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
        }
        return result;
	}
}

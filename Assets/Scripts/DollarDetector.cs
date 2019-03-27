using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WobbrockLib;
using WobbrockLib.Extensions;

public class DollarDetector : IFigureDetection {

    private int MAX_SIZE = 200;

    private List<TimePointF> _timePointsHeight;
    private List<TimePointF> _timePointsRoll;
    private List<TimePointF> _timePointsPitch;
    private List<TimePointF> _timePointsYaw;

    Recognizer _recHeight = new Recognizer();
    Recognizer _recRoll = new Recognizer();
    Recognizer _recPitch = new Recognizer();
    Recognizer _recYaw = new Recognizer();

    int time = 0;

    public DollarDetector()
    {
        FigureLoader loader = new FigureLoader(_recHeight, _recRoll, _recPitch, _recYaw);
        loader.LoadFigures();
        _timePointsHeight = new List<TimePointF>(MAX_SIZE);
        _timePointsRoll = new List<TimePointF>(MAX_SIZE);
        _timePointsPitch = new List<TimePointF>(MAX_SIZE);
        _timePointsYaw = new List<TimePointF>(MAX_SIZE);
}

    public void setPoint(IFlyingObject plane)
    {

    }

    public void setPoint(Coordinate point) {
        if (_timePointsHeight.Count == MAX_SIZE)
        {
            int i;
            for(i=0; i < _timePointsHeight.Count-2; i++)
            {
                _timePointsHeight.RemoveAt(i);
                _timePointsRoll.RemoveAt(i);
                _timePointsPitch.RemoveAt(i);
                _timePointsYaw.RemoveAt(i);
                
                TimePointF height = _timePointsHeight[i + 1];
                TimePointF roll = _timePointsRoll[i + 1];
                TimePointF pitch = _timePointsPitch[i + 1];
                TimePointF yaw = _timePointsYaw[i + 1];

                height.X--;
                roll.X--;
                pitch.X--;
                yaw.X--;

                _timePointsHeight.Insert(i, height);
                _timePointsRoll.Insert(i, roll);
                _timePointsPitch.Insert(i, pitch);
                _timePointsYaw.Insert(i, yaw);
            }
            _timePointsHeight.RemoveAt(i);
            _timePointsRoll.RemoveAt(i);
            _timePointsPitch.RemoveAt(i);
            _timePointsYaw.RemoveAt(i);
        }
        time++;
        _timePointsHeight.Add(new TimePointF(time, point.ypos, point.time));
        _timePointsYaw.Add(new TimePointF(time, point.yaw, point.time));
        _timePointsPitch.Add(new TimePointF(time, point.pitch, point.time));
        _timePointsRoll.Add(new TimePointF(time, point.roll, point.time));
        
    }

    private void ClearLists()
    {
        _timePointsHeight.Clear();
        _timePointsRoll.Clear();
        _timePointsPitch.Clear();
        _timePointsYaw.Clear();
        time = 0;
    }

    private bool AnalyseResults(NBestList resultHeight, NBestList resultRoll, NBestList resultPitch, NBestList resultYaw, string height, string roll, string pitch, string yaw)
    {
        return (resultHeight.Name.Equals(height) && resultHeight.Score > 0.7f
            && resultRoll.Name.Equals(roll) && resultRoll.Score > 0.7f
            && resultPitch.Name.Equals(pitch) && resultPitch.Score > 0.7f
            && resultYaw.Name.Equals(yaw) && resultYaw.Score > 0.7f);
    }

	public List<Figure> detection() {
        NBestList resultHeight = _recHeight.Recognize(_timePointsHeight, false);
        NBestList resultRoll = _recRoll.Recognize(_timePointsRoll, false);
        NBestList resultPitch = _recPitch.Recognize(_timePointsPitch, false);
        NBestList resultYaw = _recYaw.Recognize(_timePointsYaw, false);

        //Debug.Log(resultHeight.Name + ", " + resultRoll.Name + ", " + resultPitch.Name + ", " + resultYaw.Name);

        List<Figure> result = new List<Figure>();

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
        if (AnalyseResults(resultHeight, resultRoll, resultPitch, resultYaw,"LigneDroite", "LigneMontante", "LigneDroite", "LigneDroite"))
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

        return result;
	}
}

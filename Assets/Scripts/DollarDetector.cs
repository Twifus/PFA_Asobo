using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WobbrockLib;
using WobbrockLib.Extensions;

public class DollarDetector : IFigureDetection {

    private List<TimePointF> _timePointsHeight = new List<TimePointF>(1000);
    private List<TimePointF> _timePointsRoll = new List<TimePointF>(1000);
    private List<TimePointF> _timePointsPitch = new List<TimePointF>(1000);
    private List<TimePointF> _timePointsYaw = new List<TimePointF>(1000);

    Recognizer _recHeight = new Recognizer();
    Recognizer _recRoll = new Recognizer();
    Recognizer _recPitch = new Recognizer();
    Recognizer _recYaw = new Recognizer();

    public DollarDetector()
    {
        FigureLoader loader = new FigureLoader(_recHeight, _recRoll, _recPitch, _recYaw);
        loader.LoadFigures();
    }


    public void setPoint(Coordinate point) {
        if (_timePointsHeight.Count == 1000)
        {
            for(int i=0; i < _timePointsHeight.Count-2; i++)
            {
                _timePointsHeight.RemoveAt(i);
                _timePointsHeight.Insert(i, _timePointsHeight[i + 1]);
                _timePointsRoll.RemoveAt(i);
                _timePointsRoll.Insert(i, _timePointsRoll[i + 1]);
                _timePointsPitch.RemoveAt(i);
                _timePointsPitch.Insert(i, _timePointsPitch[i + 1]);
                _timePointsYaw.RemoveAt(i);
                _timePointsYaw.Insert(i, _timePointsYaw[i + 1]);
            }
        }
        _timePointsHeight.Add(new TimePointF(point.time, point.ypos, point.time));
        _timePointsYaw.Add(new TimePointF(point.time, point.yaw, point.time));
        _timePointsPitch.Add(new TimePointF(point.time, point.pitch, point.time));
        _timePointsRoll.Add(new TimePointF(point.time, point.roll, point.time));
        
    }

    private void ClearLists()
    {
        _timePointsHeight.Clear();
        _timePointsRoll.Clear();
        _timePointsPitch.Clear();
        _timePointsYaw.Clear();
    }

    private bool AnalyseResults(NBestList resultHeight, NBestList resultRoll, NBestList resultPitch, NBestList resultYaw, string height, string roll, string pitch, string yaw)
    {
        return (resultHeight.Name.Equals(height) && resultHeight.Score > 0.6f
            && resultRoll.Name.Equals(roll) && resultRoll.Score > 0.6f
            && resultPitch.Name.Equals(pitch) && resultPitch.Score > 0.6f
            && resultYaw.Name.Equals(yaw) && resultYaw.Score > 0.6f);
    }

	public List<Figure> detection() {
        NBestList resultHeight = _recHeight.Recognize(_timePointsHeight, false);
        NBestList resultRoll = _recRoll.Recognize(_timePointsRoll, false);
        NBestList resultPitch = _recPitch.Recognize(_timePointsPitch, false);
        NBestList resultYaw = _recYaw.Recognize(_timePointsYaw, false);

        //Debug.Log(resultHeight.Name + ", " + resultRoll.Name + ", " + resultPitch.Name + ", " + resultYaw.Name);

        List<Figure> result = new List<Figure>();

        // Loop
        if (AnalyseResults(resultHeight, resultRoll, resultPitch, resultYaw, "Bosse", "LigneDroite", "ZigZag", "LigneCoupee"))
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

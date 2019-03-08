using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WobbrockLib;
using WobbrockLib.Extensions;

public class DollarDetector : IFigureDetection {

    private List<TimePointF> _timePointsHeight = new List<TimePointF>();
    private List<TimePointF> _timePointsRoll = new List<TimePointF>();
    private List<TimePointF> _timePointsPitch = new List<TimePointF>();
    private List<TimePointF> _timePointsYaw = new List<TimePointF>();

    Recognizer _rec = new Recognizer();

    public void initialize()
    {
        FigureLoader loader = new FigureLoader(_rec);
        loader.LoadFigures();
    }


    public void setPoint(Coordinate point) {
        _timePointsHeight.Add(new TimePointF(point.time, point.ypos, point.time));
        _timePointsYaw.Add(new TimePointF(point.time, point.xangle, point.time));
        _timePointsPitch.Add(new TimePointF(point.time, point.yangle, point.time));
        _timePointsRoll.Add(new TimePointF(point.time, point.zangle, point.time));
    }
	
	public List<Figure> detection() {
        NBestList resultHeight = _rec.Recognize(_timePointsHeight, false);
        NBestList resultRoll = _rec.Recognize(_timePointsRoll, false);
        NBestList resultPitch = _rec.Recognize(_timePointsPitch, false);
        NBestList resultYaw = _rec.Recognize(_timePointsYaw, false);

        List<Figure> result = new List<Figure>();

        if (resultHeight.Name.Equals("Bosse") && resultRoll.Name.Equals("LigneDroite")
            && resultPitch.Name.Equals("ZigZag") && resultYaw.Name.Equals("LigneCoupee"))
        {
            result.Add(new Figure());
            result[0].id = figure_id.LOOP;
            result[0].quality = 1f;
            _timePointsHeight.Clear();
            _timePointsRoll.Clear();
            _timePointsPitch.Clear();
            _timePointsYaw.Clear();
        }

        if (resultHeight.Name.Equals("LigneDroite") && resultRoll.Name.Equals("LigneMontante")
            && resultPitch.Name.Equals("LigneDroite") && resultYaw.Name.Equals("LigneDroite"))
        {
            result.Add(new Figure());
            result[0].id = figure_id.BARREL;
            result[0].quality = 1f;
            _timePointsHeight.Clear();
            _timePointsRoll.Clear();
            _timePointsPitch.Clear();
            _timePointsYaw.Clear();
        }

        if (resultHeight.Name.Equals("DoubleBosse") && resultRoll.Name.Equals("DoubleDemieLigneMontante")
            && resultPitch.Name.Equals("DoubleZigZag") && resultYaw.Name.Equals("LigneCoupee"))
        {
            result.Add(new Figure());
            result[0].id = figure_id.CUBANEIGHT;
            result[0].quality = 1f;
            _timePointsHeight.Clear();
            _timePointsRoll.Clear();
            _timePointsPitch.Clear();
            _timePointsYaw.Clear();
        }

        return result;
	}
}

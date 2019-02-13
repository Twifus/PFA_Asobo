using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureFaussaire: IFigureDetection{

	// Use this for initialization
	void Start () {
    }

    public void analyzeLoop(List<Figure> _listFig)
    {
        if (Time.time % 7f < 0.5f)
        {
            _listFig[0].quality = 1f;
        }
        else
            _listFig[0].quality = 0f;
    }

    public void analyzeBarrel(List<Figure> _listFig)
    {
        if (Time.time % 3f < 0.5f)
        {
            _listFig[1].quality = 1f;
        }
        else
            _listFig[1].quality = 0f;
    }

    public void analyzeCubanEight(List<Figure> _listFig)
    {
        if (Time.time % 29f < 0.1f)
        {
            _listFig[2].quality = 1f;
        }
        else
            _listFig[2].quality = 0f;
    }

    public void setPoint(Coordinate point)
    {

    }

    public List<Figure> detection()
    {
        List<Figure>  _listFig = new List<Figure>();
        _listFig.Add(new Figure());
        _listFig[0].id = figure_id.LOOP;
        _listFig.Add(new Figure());
        _listFig[1].id = figure_id.BARREL;
        _listFig.Add(new Figure());
        _listFig[2].id = figure_id.CUBANEIGHT;

        analyzeBarrel(_listFig);
        analyzeCubanEight(_listFig);
        analyzeLoop(_listFig);

        return _listFig;
    }
}

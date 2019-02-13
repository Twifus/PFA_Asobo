using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureFaussaire: IFigureDetection{
    private float _timeBarrel;
    private float _timeLoop;
    private float _timeCuban;

    private List<Figure> _listFig;


	// Use this for initialization
	void Start () {
        _timeBarrel = Time.time;
        _timeLoop = _timeBarrel;
        _timeCuban = _timeBarrel;

        _listFig.Add(new Figure());
        _listFig[0].id = figure_id.LOOP;
        _listFig.Add(new Figure());
        _listFig[1].id = figure_id.BARREL;
        _listFig.Add(new Figure());
        _listFig[2].id = figure_id.CUBANEIGHT;

    }

    public void analyzeLoop()
    {
        _timeLoop = Time.time;
        if (_timeLoop % 5f < 0.5)
        {
            _listFig[0].quality = 1f;
        }
        _listFig[0].quality = 0f;
    }

    public void analyzeBarrel()
    {
        _timeBarrel = Time.time;
        if (_timeBarrel % 3f < 0.5)
        {
            _listFig[1].quality = 1f;
        }
        _listFig[1].quality = 0f;
    }

    public void analyzeCubanEight()
    {
        _timeCuban = Time.time;
        if (_timeCuban % 29f < 0.1)
        {
            _listFig[2].quality = 1f;
        }
        _listFig[2].quality = 0f;
    }

    public void setPoint(Coordinate point)
    {

    }

    public List<Figure> detection()
    {
        analyzeBarrel();
        analyzeCubanEight();
        analyzeLoop();
        return _listFig;
    }
}

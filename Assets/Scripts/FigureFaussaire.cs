using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureFaussaire: FigureDetection{
    private float timeBarrel;
    private float timeLoop;
    private float timeCuban;

	// Use this for initialization
	void Start () {
        timeBarrel = Time.time;
        timeLoop = timeBarrel;
        timeCuban = timeBarrel;
	}

    public bool analyzeLoop()
    {
        timeLoop = Time.time;
        if (timeLoop % 5f < 0.5)
        {
            return true;
        }
        return false;
    }

    public bool analyzeBarrel()
    {
        timeBarrel = Time.time;
        if (timeBarrel % 3f < 0.5)
        {
            return true;
        }
        return false;
    }

    public bool analyzeCubanEight()
    {
        timeCuban = Time.time;
        if (timeCuban % 29f < 0.1)
        {
            return true;
        }
        return false;
    }
}

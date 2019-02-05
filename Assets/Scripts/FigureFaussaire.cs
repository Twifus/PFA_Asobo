using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureFaussaire: FigureDetection{
    private float timeBarrel;
    private float timeLoop;


	// Use this for initialization
	void Start () {
        timeBarrel = Time.time;
        timeLoop = timeBarrel;
	}
	
	// Update is called once per frame
	void Update () {
		if(timeBarrel % 3 == 0)
        {
            analyzeBarrel();
        }

        if(timeLoop % 5 == 0)
        {
            analyzeLoop();
        }
	}

    public bool analyzeLoop()
    {
        if (timeLoop % 5 == 0)
        {
            return true;
        }
        return false;
    }

    public bool analyzeBarrel()
    {
        if (timeBarrel % 3 == 0)
        {
            return true;
        }
        return false;
    }
}

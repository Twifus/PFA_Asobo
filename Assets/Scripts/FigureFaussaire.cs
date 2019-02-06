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
        timeBarrel = Time.time;
        timeLoop = Time.time;
        Debug.Log(timeBarrel);
        //print(timeBarrel);

        analyzeBarrel();
        analyzeLoop();
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
}

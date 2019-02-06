using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour {

    public Text FigureText;
    public GameObject FigureRecorder;

	// Use this for initialization
	void Start () {
        DisableText();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (FigureRecorder.GetComponent<LoopingDetection>().LoopDone)
        {
            FigureText.text = "LOOPING";
            Invoke("DisableText", 3f);
        }
        
    }

    void DisableText()
    {
        FigureText.text = "";
    }

    void DisplayFigure(string figure)
    {
        FigureText.text = figure;
        Invoke("DisableText", 2f);
    }
}

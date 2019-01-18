using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HutongGames.PlayMaker;

public class DisplayText : MonoBehaviour {

    public Text FigureText;
    public GameObject XRecorder;
    public GameObject YRecorder;
    public GameObject ZRecorder;

    // Use this for initialization
    void Start () {
        DisableText();
	}
	
	// Update is called once per frame
	void Update () {
        if (YRecorder.GetComponent<YDetection>().LoopDone)
        {
            FigureText.text = "LOOPING";
            Invoke("DisableText", 3f);
        }
        if (ZRecorder.GetComponent<ZDetection>().BarrelDone)
        {
            FigureText.text = "BARREL";
            Invoke("DisableText", 3f);
        }
    }

    void DisableText()
    {
        FigureText.text = "";
    }
}

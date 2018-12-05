using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour {

    public Text FigureText;
    private bool _loop;
    public GameObject FigureRecorder;

	// Use this for initialization
	void Start () {
        Invoke("DisableText", 0);
        _loop = FigureRecorder.GetComponent<LoopDetect>().LoopDone;
        Debug.Log(_loop);
	}
	
	// Update is called once per frame
	void Update () {
        _loop = FigureRecorder.GetComponent<LoopDetect>().LoopDone;

        if (_loop)
        {
            FigureText.text = "LOOPING";
            Invoke("DisableText", 3f);
        }

    }

    void DisableText()
    {
        FigureText.text = "";
    }
}

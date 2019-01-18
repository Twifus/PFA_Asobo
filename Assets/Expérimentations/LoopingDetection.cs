using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingDetection : MonoBehaviour {

    public GameObject Plane;
    private int _state;
    public bool LoopDone;
    private bool _check;

	// Use this for initialization
	void Start () {
        _state = 0;
	}
	
	// Update is called once per frame
	void Update () {
        LoopDone = false;
        _check = false;
        for (int i = 0; i <= 3; i++)
        {
            if (Between(Mathf.Abs(Plane.transform.eulerAngles.z), 100*i, 100*(i+1)) && (_state == i || _state == i + 1))
            {
                _state = i + 1;
                //Debug.Log(_state);
                _check = true;
            }
        }

        if (!_check)
        {
            _state = 0;
        }

        if (_state == 4)
        {
            _state = 0;
            LoopDone = true;
        }
       
    }


    private bool Between(float x, float a, float b)
    {
        return (a <= x && x < b);
    }
}

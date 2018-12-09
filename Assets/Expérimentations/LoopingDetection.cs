using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingDetection : MonoBehaviour {

    public GameObject Plane;
    private int _state;
    public bool LoopDone;

	// Use this for initialization
	void Start () {
        _state = 0;
	}
	
	// Update is called once per frame
	void Update () {
        LoopDone = false;
		//Debug.Log(Plane.transform.eulerAngles.z);

        if (Mathf.Abs(Plane.transform.eulerAngles.z) < 20 && (_state == 0|| _state == 1))
        {
            _state = 1;
            Debug.Log("Loop : 1");
        } else
        {
            if (20 <= Mathf.Abs(Plane.transform.eulerAngles.z) && Mathf.Abs(Plane.transform.eulerAngles.z) < 90 && (_state == 1 || _state == 2))
            {
                _state = 2;
                Debug.Log("Loop : 2");
            }
            else
            {
                if (90 <= Mathf.Abs(Plane.transform.eulerAngles.z) && Mathf.Abs(Plane.transform.eulerAngles.z) < 200 && (_state == 2 || _state == 3))
                {
                    _state = 3;
                    Debug.Log("Loop : 3");
                }
                else
                {
                    if (200 <= Mathf.Abs(Plane.transform.eulerAngles.z) && Mathf.Abs(Plane.transform.eulerAngles.z) < 340 && (_state == 3 || _state == 4))
                    {
                        _state = 4;
                        Debug.Log("Loop : 4");
                    }
                    else
                    {
                        if (340 <= Mathf.Abs(Plane.transform.eulerAngles.z) && _state == 4 )
                        {
                            Debug.Log("You did a Looping !");
                            _state = 0;
                            LoopDone = true;
                        } 
                        else
                        {
                            _state = 0;
                        }
                    }
                }
            }
        }

        

    }
}

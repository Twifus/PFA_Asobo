using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopDetect : MonoBehaviour {

    public GameObject plane;

    private int state = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (plane.transform.eulerAngles.z <= 20 && (state == 0 || state == 1))
        {
            Debug.Log("Loop : 1");
            state = 1;
        }
        else
        {
            if (20 < plane.transform.eulerAngles.z && plane.transform.eulerAngles.z <= 100 && (state == 1 || state == 2))
            {
                Debug.Log("Loop : 2");
                state = 2;
            }
            else
            {
                if (100 < plane.transform.eulerAngles.z && plane.transform.eulerAngles.z <= 200 && (state == 2 || state == 3))
                {
                    Debug.Log("Loop : 3");
                    state = 3;
                }
                else
                {
                    if (200 < plane.transform.eulerAngles.z && plane.transform.eulerAngles.z <= 350 && (state == 3 || state == 4))
                    {
                        Debug.Log("Loop : 4");
                        state = 4;
                    }
                    else
                    {
                        if (350 <= plane.transform.eulerAngles.z && state == 4)
                        {
                            Debug.Log("LOOOOOP");
                            Debug.Log("LOOOOOP");
                            Debug.Log("LOOOOOP");
                            Debug.Log("LOOOOOP");
                            state = 0;
                        }
                        else
                        {
                            state = 0;
                        }
                    }
                }
            }
        }
	}
}

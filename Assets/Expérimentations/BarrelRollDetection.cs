using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelRollDetection : MonoBehaviour  //NE MARCHE PAS
{

    public GameObject Plane;
    int State;

    // Use this for initialization
    void Start()
    {
        State = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Debug.Log(Plane.transform.eulerAngles.x);
        Debug.Log(Plane.transform.eulerAngles.y);
        Debug.Log(Plane.transform.eulerAngles.z);
        */

        if (Mathf.Abs(Plane.transform.eulerAngles.x) < 20 && (State == 0 || State == 1))
        {
            State = 1;
            Debug.Log("Loop : 1");
        }
        else
        {
            if (20 <= Mathf.Abs(Plane.transform.eulerAngles.x) && Mathf.Abs(Plane.transform.eulerAngles.x) < 90 && (State == 1 || State == 2))
            {
                State = 2;
                Debug.Log("Loop : 2");
            }
            else
            {
                if (90 <= Mathf.Abs(Plane.transform.eulerAngles.x) && Mathf.Abs(Plane.transform.eulerAngles.x) < 200 && (State == 2 || State == 3))
                {
                    State = 3;
                    Debug.Log("Loop : 3");
                }
                else
                {
                    if (200 <= Mathf.Abs(Plane.transform.eulerAngles.x) && Mathf.Abs(Plane.transform.eulerAngles.x) < 340 && (State == 3 || State == 4))
                    {
                        State = 4;
                        Debug.Log("Loop : 4");
                    }
                    else
                    {
                        if (340 <= Mathf.Abs(Plane.transform.eulerAngles.x) && State == 4)
                        {
                            Debug.Log("Do a BarrelRoll !");
                            Debug.Log("Do a BarrelRoll !");
                            Debug.Log("Do a BarrelRoll !");
                            Debug.Log("Do a BarrelRoll !");
                            Debug.Log("Do a BarrelRoll !");
                            State = 0;
                        }
                        else
                        {
                            State = 0;
                        }
                    }
                }
            }
        }
    }
}

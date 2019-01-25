using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class ZDetection : MonoBehaviour
{

    public PlayMakerFSM Fsm;
    public GameObject Plane;
    public bool BarrelDone;

    void Update()
    {
        // getting fsm variables by name
        FsmFloat PlaneRot = Fsm.FsmVariables.GetFsmFloat("PlaneRot");
        FsmBool FsmBarrelDone = Fsm.FsmVariables.GetFsmBool("BarrelDone");

        BarrelDone = FsmBarrelDone.Value;

        // setting fsm variable value
        float roll = Vector3.Angle(Vector3.up, Plane.transform.up);
        if (Plane.transform.right.y < 0) //tofix
        {
            roll = 360 - roll;
        }
        PlaneRot.Value = roll; // Plane.transform.eulerAngles.z;
        Debug.Log("Zangle :" + PlaneRot.Value);
        Debug.Log("ZeulerAngles :" + Plane.transform.localEulerAngles.z);

        // sending an event
        //fsm.SendEvent("myEvent");

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class YDetection : MonoBehaviour
{

    public PlayMakerFSM Fsm;
    public GameObject Plane;
    public bool LoopDone;

    void Update()
    {
        // getting fsm variables by name
        FsmFloat PlaneRot = Fsm.FsmVariables.GetFsmFloat("PlaneRot");
        FsmBool FsmLoopDone = Fsm.FsmVariables.GetFsmBool("LoopDone");

        LoopDone = FsmLoopDone.Value;

        // setting fsm variable value
        float pitch = Vector3.Angle(Vector3.up, Plane.transform.up);
        if (Plane.transform.forward.y < 0)
        {
            pitch = 360 - pitch;
        }
        PlaneRot.Value = pitch; // Plane.transform.eulerAngles.z;
        Debug.Log("Yangle :" + PlaneRot.Value);
        Debug.Log("YeulerAngles :" + Plane.transform.localEulerAngles.y);


        // sending an event
        //fsm.SendEvent("myEvent");

    }
}
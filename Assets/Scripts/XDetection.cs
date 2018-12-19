using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class XDetection : MonoBehaviour
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
        float yaw = Vector3.Angle(Vector3.right, Plane.transform.right);
        if (Plane.transform.right.x < 0)
        {
            yaw = 360 - yaw;
        }
        PlaneRot.Value = yaw; // Plane.transform.eulerAngles.z;
        Debug.Log(PlaneRot.Value);

        // sending an event
        //fsm.SendEvent("myEvent");

    }
}

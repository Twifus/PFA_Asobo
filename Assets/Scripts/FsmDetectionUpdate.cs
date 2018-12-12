using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class FsmDetectionUpdate : MonoBehaviour {

    public PlayMakerFSM Fsm;
    public GameObject Plane;

    // Update is called once per frame
    void Update () {
        // getting fsm variables by name
        FsmFloat PlaneRot = Fsm.FsmVariables.GetFsmFloat("PlaneRot");

        // setting fsm variable value
        float pitch = Vector3.Angle(Vector3.up, Plane.transform.up);
        if (Plane.transform.right.y < 0)
        {
            pitch = 360 - pitch;
        }
        PlaneRot.Value = pitch; // Plane.transform.eulerAngles.z;
        Debug.Log(PlaneRot.Value);

        // sending an event
        //fsm.SendEvent("myEvent");

    }
}

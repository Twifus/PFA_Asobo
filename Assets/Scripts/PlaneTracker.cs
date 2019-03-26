using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using s = System.Numerics;

public class PlaneTracker : MonoBehaviour {

    private StreamWriter FileWriter;
    private Plane Plane;
    public GameObject Player;

    private float ts;
    private bool buttonPress;
    private bool record;

    public void Start()
    {
        Plane = Plane.NewPlane(Player);
    }

    private void OnApplicationQuit()
    {
        if (FileWriter != null)
            FileWriter.Close();
    }

    private void Update()
    {
        if (Input.GetButtonDown("ToggleRecord"))
        {
            ts = Time.time;
            buttonPress = true;
        }

        if (buttonPress && Input.GetButton("ToggleRecord") && Time.time > ts + 0.3f)
        {
            if (!record)
            {
                string path = string.Format("../Figure-{0}", System.DateTime.Now.ToFileTime());
                FileWriter = new StreamWriter(path + "-Input.csv", true);
                Debug.Log("Record started - Writting at " + path);
            }
            else
            {
                FileWriter.Close();
                Debug.Log("Record stopped");
            }
            record = !record;
            buttonPress = false;
        }
    }

    private void FixedUpdate()
    {
        if (record)
        {
            FileWriter.WriteLine(
                string.Format(
                    "{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18}",
                    Time.time,
                    Plane.pos.X, Plane.pos.Y, Plane.pos.Z,
                    Plane.speed.X, Plane.speed.Y, Plane.speed.Z,
                    Plane.roll, Plane.pitch, Plane.yaw,
                    s.Vector3.Dot(Plane.right, s.Vector3.UnitX), s.Vector3.Dot(Plane.right, s.Vector3.UnitY), s.Vector3.Dot(Plane.right, s.Vector3.UnitZ),
                    s.Vector3.Dot(Plane.up, s.Vector3.UnitX), s.Vector3.Dot(Plane.up, s.Vector3.UnitY), s.Vector3.Dot(Plane.up, s.Vector3.UnitZ),
                    s.Vector3.Dot(Plane.forward, s.Vector3.UnitX), s.Vector3.Dot(Plane.forward, s.Vector3.UnitY), s.Vector3.Dot(Plane.forward, s.Vector3.UnitZ)));
        }
    }

}

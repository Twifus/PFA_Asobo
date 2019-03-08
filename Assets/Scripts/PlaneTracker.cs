using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlaneTracker : MonoBehaviour {

    private StreamWriter InputWriter;
    private StreamWriter TrajWriter;
    private Plane Plane;
    public GameObject Player;

    private float ts;
    private bool buttonPress;
    private bool record;

    public void Start()
    {
        string path = string.Format("../Figure-{0}", System.DateTime.Now.ToFileTime());
        InputWriter = new StreamWriter(path + "-Input.csv", true);
        TrajWriter = new StreamWriter(path + "-Traj.csv", true);
        Plane = Plane.NewPlane(Player);
        Debug.Log("Traj log file : " + path);
    }

    private void OnApplicationQuit()
    {
        InputWriter.Close();
        TrajWriter.Close();
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
                Debug.Log("Record started");
            else
                Debug.Log("Record stopped");
            record = !record;
            buttonPress = false;
        }

        if (record)
        {
            TrajWriter.WriteLine(string.Format("{0};{1};{2};{3};{4}",
                Time.time, Plane.Position.y, Plane.Roll, Plane.Pitch, Plane.Yaw));

            InputWriter.WriteLine(string.Format("{0};{1};{2};{3};{4}",
                Time.time, CustomInput.GetAxis("Accelerate"), CustomInput.GetAxis("Roll"), CustomInput.GetAxis("Pitch"), CustomInput.GetAxis("Yaw")));
        }
    }

}

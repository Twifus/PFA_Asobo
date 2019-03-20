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
        Plane = Plane.NewPlane(Player);
    }

    private void OnApplicationQuit()
    {
        if (InputWriter != null)
            InputWriter.Close();
        if (TrajWriter != null)
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
            {
                string path = string.Format("../Figure-{0}", System.DateTime.Now.ToFileTime());
                InputWriter = new StreamWriter(path + "-Input.csv", true);
                TrajWriter = new StreamWriter(path + "-Traj.csv", true);
                Debug.Log("Record started - Writting at " + path);
            }
            else
            {
                InputWriter.Close();
                TrajWriter.Close();
                Debug.Log("Record stopped");
            }
            record = !record;
            buttonPress = false;
        }

        if (record)
        {
            TrajWriter.WriteLine(string.Format("{0};{1};{2};{3};{4}",
                Time.time, Plane.Position.y, Plane.roll, Plane.pitch, Plane.yaw));

            InputWriter.WriteLine(string.Format("{0};{1};{2};{3};{4}",
                Time.time, CustomInput.GetAxis("Accelerate"), CustomInput.GetAxis("Roll"), CustomInput.GetAxis("Pitch"), CustomInput.GetAxis("Yaw")));
        }
    }

}

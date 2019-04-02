using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using s = System.Numerics;
using System.Globalization;

public class PlaneTracker : MonoBehaviour {

    private StreamWriter FileWriter;
    private Plane Plane;
    public GameObject Player;

    public RawImage recordImage;
    private float blinkTime;
    private float blinkDelay = 0.5f;

    private float ts;
    private bool buttonPress;
    private bool record;

    public void Start()
    {
        Plane = Plane.NewPlane(Player);
        recordImage.enabled = false;
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
                string path = string.Format("../Record-{0}", System.DateTime.Now.ToFileTime());
                FileWriter = new StreamWriter(path + ".csv", true);
                FileWriter.WriteLine(
                    "Time;" +
                    "X;Y;Z;" +
                    "Vx;Vy;Vz;" +
                    "Roll;Pitch;Yaw;" +
                    "pRight.wRight;pRight.wUp;pRight.wForward;" +
                    "pUp.wRight;pUp.wUp;pUp.wForward;" +
                    "pForward.wRight;pForward.wUp;pForward.wForward;" +
                    "ScalarRoll;ScalarPitch;ScalarYaw;" +
                    "InputAccelerate;" +
                    "InputRoll;InputPitch;InputRoll");
                recordImage.enabled = true;
                blinkTime = Time.time;
                Debug.Log("Record started - Writting at " + path);
            }
            else
            {
                FileWriter.Close();
                recordImage.enabled = false;
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
            if (Time.time > blinkTime + blinkDelay)
            {
                recordImage.enabled = !recordImage.enabled;
                blinkTime = Time.time;
            }
            // Convert data to invariable culture strings
            float[] data = {
                Time.time,
                Plane.pos.X, Plane.pos.Y, Plane.pos.Z,
                Plane.speed.X, Plane.speed.Y, Plane.speed.Z,
                Plane.roll, Plane.pitch, Plane.yaw,
                s.Vector3.Dot(Plane.right, s.Vector3.UnitX), s.Vector3.Dot(Plane.right, s.Vector3.UnitY), s.Vector3.Dot(Plane.right, s.Vector3.UnitZ),
                s.Vector3.Dot(Plane.up, s.Vector3.UnitX), s.Vector3.Dot(Plane.up, s.Vector3.UnitY), s.Vector3.Dot(Plane.up, s.Vector3.UnitZ),
                s.Vector3.Dot(Plane.forward, s.Vector3.UnitX), s.Vector3.Dot(Plane.forward, s.Vector3.UnitY), s.Vector3.Dot(Plane.forward, s.Vector3.UnitZ),
                Plane.rollScalar, Plane.pitchScalar, Plane.yawScalar,
                CustomInput.GetAxis("Accelerate"), CustomInput.GetAxis("Roll"), CustomInput.GetAxis("Pitch"), CustomInput.GetAxis("Yaw")
            };
            List<string> sdata = new List<string>();
            foreach(float f in data)
            {
                sdata.Add(f.ToString(CultureInfo.InvariantCulture));
            }
            // Write in file
            FileWriter.WriteLine(string.Join(";", sdata));
        }
    }

}

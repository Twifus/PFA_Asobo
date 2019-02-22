using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlaneTracker : MonoBehaviour {

    private StreamWriter Writer;
    private Plane Plane;
    public GameObject Player;

    public void Start()
    {
        string path = string.Format("../Figure-{0}.csv", System.DateTime.Now.ToFileTime());
        Writer = new StreamWriter(path, true);
        Plane = Plane.NewPlane(Player);
        Debug.Log("Writting in " + path);
    }

    private void OnApplicationQuit()
    {
        Writer.Close();
    }

    private void Update()
    {
        //Debug.Log(string.Format("{0};{1};{2};{3};{4}", Time.time, Plane.Position.z, Plane.Roll, Plane.Pitch, Plane.Yaw));
        Writer.WriteLine(string.Format("{0};{1};{2};{3};{4}", Time.time, Plane.Position.y, Plane.Roll, Plane.Pitch, Plane.Yaw));
    }

}

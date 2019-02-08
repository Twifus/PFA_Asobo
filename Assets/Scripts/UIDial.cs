using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDial : MonoBehaviour {

    public GameObject plane;
    public GameObject rollImage;
    public GameObject pitchImage;
    public GameObject altitudeText;
    private Plane _plane;
    private Transform rollTransform;
    private Transform pitchTransform;
    private Text altitude;

    // Use this for initialization
    void Start() {
        _plane = Plane.NewPlane(plane);
        rollTransform = rollImage.GetComponent<RectTransform>();
        pitchTransform = pitchImage.GetComponent<RectTransform>();
        altitude = altitudeText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        rollTransform.rotation = Quaternion.Euler(0, 0, _plane.Rotation.eulerAngles.z);

        //Vector3 planeForward = _plane.Rigidbody.transform.forward;
        //Vector3 vectorOnPlane;
        //if (Vector3.Dot(Vector3.up, _plane.Rigidbody.transform.up) > 0) { 
        //    vectorOnPlane = Vector3.ProjectOnPlane(_plane.Rigidbody.transform.forward, Vector3.up);
        //}
        //else { // Plane is upside down
        //    vectorOnPlane = Vector3.ProjectOnPlane(-_plane.Rigidbody.transform.forward, Vector3.up);
        //}
        //var angle = Vector3.SignedAngle(planeForward, vectorOnPlane, _plane.Rigidbody.transform.right);

        var angle = _plane.Pitch;
        if (Vector3.Dot(Vector3.up, _plane.Rigidbody.transform.up) < 0) {
            angle -= 180;
        }

        //Debug.Log(angle);
        pitchTransform.rotation = Quaternion.Euler(0, 0, angle);
        altitude.text = ((int) _plane.Position.y).ToString();
        
    }
}

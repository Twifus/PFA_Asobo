using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDial : MonoBehaviour {

    public GameObject plane;
    public GameObject rollImage;
    public GameObject pitchImage;
    public Text altitudeText;
    public Text vitesseText;
    private Plane _plane;
    private Transform rollTransform;
    private Transform pitchTransform;

    // Use this for initialization
    void Start() {
        _plane = Plane.NewPlane(plane);
        rollTransform = rollImage.GetComponent<RectTransform>();
        pitchTransform = pitchImage.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
        rollTransform.rotation = Quaternion.Euler(0, 0, _plane.Rotation.eulerAngles.z);

        var angle = _plane.pitch;
        //if (Vector3.Dot(Vector3.up, _plane.Rigidbody.transform.up) < 0) {
        //    angle -= 180;
        //}

        //Debug.Log(angle);
        pitchTransform.rotation = Quaternion.Euler(0, 0, angle);
        altitudeText.text = "Altitude : " + Mathf.RoundToInt(_plane.Position.y) + " m";
        vitesseText.text = "Vitesse : " + Mathf.RoundToInt(_plane.speed.Length() * 3.6f) + " km/h";
    }
}

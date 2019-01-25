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
        var angle = Mathf.Atan2(_plane.Rigidbody.transform.forward.y, _plane.Rigidbody.transform.forward.z) * Mathf.Rad2Deg;
        angle = Vector3.SignedAngle(_plane.Rigidbody.transform.up, transform.up, _plane.Rigidbody.transform.right);
        pitchTransform.rotation = Quaternion.Euler(0, 0, angle);
        altitude.text = ((int) _plane.Position.y).ToString();
    }
}

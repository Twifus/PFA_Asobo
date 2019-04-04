using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///Affiche et calcule l'altitude et la vitesse de l'avion à l'écran
/// </summary>
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
        var angle = _plane.pitch;

        rollTransform.rotation = Quaternion.Euler(0, 0, _plane.Rotation.eulerAngles.z);
        pitchTransform.rotation = Quaternion.Euler(0, 0, angle);
        altitudeText.text = "Altitude : " + Mathf.RoundToInt(_plane.Position.y) + " m";
        vitesseText.text = "Vitesse : " + Mathf.RoundToInt(_plane.speed.Length() * 3.6f) + " km/h";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiRotation : MonoBehaviour {

    public GameObject plane;
    private Plane _plane;
    private Transform ui;

	// Use this for initialization
	void Start () {
        _plane = Plane.NewPlane(plane);
        ui = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        ui.rotation = Quaternion.Euler(0, 0, _plane.Rotation.eulerAngles.z);
    }
}

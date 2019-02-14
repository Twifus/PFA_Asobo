﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneController : MonoBehaviour
{

    public float WingArea;

    public float LiftCoeff;

    public float DragCoeff;

    public float ThrustPower;

    public static float ThrustCoeff;

    public float RollIntensity;

    public float PitchIntensity;

    public float YawIntensity;

    public float RollCoeff;
    
    private Rigidbody _body;

    public Transform CenterOfMass;
    public Transform Engine;
    public Transform LeftWing;
    public Transform RightWing;
    public Transform Tail;

    public GameObject[] PathRenderers;

    private float _airDensity = 1.184f;

    private Vector3 _thrust;
    private Vector3 _llift;
    private Vector3 _rlift;
    private Vector3 _drag;

    private Plane _plane;

    // Use this for initialization
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _body.centerOfMass = CenterOfMass.localPosition;
        _plane = Plane.NewPlane(gameObject);
        WingArea = PlaneSettings.WingArea;
        LiftCoeff = PlaneSettings.LiftCoeff;
        DragCoeff = PlaneSettings.DragCoeff;
        ThrustPower = PlaneSettings.ThrustPower;
        ThrustCoeff = PlaneSettings.ThrustCoeff;
        RollIntensity = PlaneSettings.RollIntensity;
        PitchIntensity = PlaneSettings.PitchIntensity;
        YawIntensity = PlaneSettings.YawIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("SettingsUI");
        }

        /* Lift */
        _llift = _rlift = Vector3.zero;
        Vector3 baseLift = 0.5f * LiftCoeff * _airDensity * _body.velocity.sqrMagnitude * transform.up;

        _llift = (CustomInput.GetAxis("Pitch") + RollCoeff * CustomInput.GetAxis("Roll")) * baseLift;
        _rlift = (CustomInput.GetAxis("Pitch") - RollCoeff * CustomInput.GetAxis("Roll")) * baseLift;

        /* Drag */
        _drag = -0.5f * DragCoeff * _airDensity * _body.velocity.sqrMagnitude * _body.velocity.normalized;

        /* Thrust */
        _thrust = ThrustPower * ThrustCoeff * transform.forward;
        _thrust = _thrust * CustomInput.GetAxis("Accelerate");
        
        _body.AddForceAtPosition(_thrust, Engine.position);
        _body.AddForceAtPosition(_llift, LeftWing.position);
        _body.AddForceAtPosition(_rlift, RightWing.position);
        _body.AddForce(_drag);

        //_body.AddForceAtPosition(- 0.1f * baseLift.magnitude * CustomInput.GetAxis("Yaw") * transform.right, Tail.position); // Yaw (useful?)
    }

    private void OnDrawGizmos()
    {
        Vector3 bodyPos = transform.position;
        Vector3 enginePos = Engine.transform.position;
        Vector3 lwPos = LeftWing.transform.position;
        Vector3 rwPos = RightWing.transform.position;

        Gizmos.DrawLine(enginePos, enginePos + _thrust / 1000);
        Gizmos.DrawLine(lwPos, lwPos + _llift / 1000);
        Gizmos.DrawLine(rwPos, rwPos + _rlift / 1000);
        Gizmos.DrawLine(bodyPos, bodyPos + _drag / 1000);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneController : MonoBehaviour
{
    private Rigidbody _body;

    public Transform CenterOfMass;
    public Transform Engine;
    public Transform LeftWing;
    public Transform RightWing;
    public Transform Tail;

    public GameObject explosionPrefab;

    public GameObject[] SmokeTrails;

    private Vector3 _thrust;
    private Vector3 _llift;
    private Vector3 _rlift;
    private Vector3 _drag;

    private Plane _plane;
    private bool _crashed;
    private GameObject _explosion;

    // Use this for initialization
    void Start()
    {
        _plane = Plane.NewPlane(gameObject);
        _body = _plane.Rigidbody;
        if (CenterOfMass != null)
            _body.centerOfMass = CenterOfMass.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_crashed)
        {
            /* Lift */
            _llift = _rlift = Vector3.zero;
            Vector3 baseLift = 0.5f * PlaneSettings.LiftCoeff * PlaneSettings.AirDensity * _body.velocity.sqrMagnitude * transform.up;

            _llift = (CustomInput.GetAxis("Pitch") + PlaneSettings.RollIntensity * CustomInput.GetAxis("Roll")) * baseLift;
            _rlift = (CustomInput.GetAxis("Pitch") - PlaneSettings.RollIntensity * CustomInput.GetAxis("Roll")) * baseLift;

            /* Drag */
            _drag = -0.5f * PlaneSettings.DragCoeff * PlaneSettings.AirDensity * _body.velocity.sqrMagnitude * _body.velocity.normalized;

            /* Thrust */
            _thrust = PlaneSettings.ThrustPower * PlaneSettings.ThrustMultiplier * transform.forward;
            _thrust = _thrust * CustomInput.GetAxis("Accelerate");

            _body.AddForceAtPosition(_thrust, Engine.position);
            _body.AddForceAtPosition(_llift, LeftWing.position);
            _body.AddForceAtPosition(_rlift, RightWing.position);
            _body.AddForce(_drag);

            _body.AddForceAtPosition(- PlaneSettings.YawIntensity * baseLift.magnitude * CustomInput.GetAxis("Yaw") * transform.right, Tail.position); // Yaw
        }
    }

    private void Update()
    {
        if (_crashed && !_explosion)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" && Vector3.Dot(collision.contacts[0].normal, collision.relativeVelocity) > 15f)
        {
            _explosion = Instantiate(explosionPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            _crashed = true;
        }
    }
}

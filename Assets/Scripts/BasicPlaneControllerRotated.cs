using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlaneControllerRotated : MonoBehaviour {

    [Range(0f, 100f)]
    public float WingArea;

    [Range(0f, 100f)]
    public float LiftCoeff;

    [Range(0f, 100f)]
    public float DragCoeff;

    [Range(0f, 100f)]
    public float ThrustPower;

    public float ThrustCoeff;

    [Range(0f, 360f)]
    public float RollIntensity;

    [Range(0f, 360f)]
    public float PitchIntensity;

    [Range(0f, 360f)]
    public float YawIntensity;

    private Rigidbody _rb;
    private float _airDensity = 1.184f;
    private int _lastHeigth;

    // Use this for initialization
    void Start () {
        _rb = GetComponent<Rigidbody>();
        _lastHeigth = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y >= _lastHeigth + 100)
        {
            _lastHeigth = _lastHeigth + 100;
            Debug.Log("Altitude : " + _lastHeigth);
        }

        float dynamicLiftCoeff = LiftCoeff * transform.up.y;

        //Debug.Log(dynamicLiftCoeff);

        Vector3 lift = 0.5f * dynamicLiftCoeff * _airDensity * _rb.velocity.sqrMagnitude * transform.up;
        Vector3 drag = -0.5f * DragCoeff * _airDensity * _rb.velocity.sqrMagnitude * _rb.velocity.normalized;
        Vector3 thrust = ThrustPower * ThrustCoeff * transform.right;

        if (Input.GetButton("Roll"))
            transform.Rotate(Vector3.right * Time.deltaTime * RollIntensity * -Input.GetAxis("Roll"));

        if (Input.GetButton("Pitch"))
            transform.Rotate(Vector3.up * Time.deltaTime * PitchIntensity * -Input.GetAxis("Pitch"));
        
        if (Input.GetButton("Yaw"))
            transform.Rotate(Vector3.forward * Time.deltaTime * YawIntensity * Input.GetAxis("Yaw"));
        
        thrust = thrust * Input.GetAxis("Accelerate");

        _rb.AddForce(lift);
        _rb.AddForce(drag);
        _rb.AddForce(thrust);
    }
}

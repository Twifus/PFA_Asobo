using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlaneController : MonoBehaviour {

    [Range(0f, 100f)]
    public float wingArea;

    [Range(0f, 100f)]
    public float liftCoeff;

    [Range(0f, 100f)]
    public float dragCoeff;

    [Range(0f, 100f)]
    public float thrustPower;

    public float thrustCoeff;

    [Range(0f, 360f)]
    public float rollIntensity;

    [Range(0f, 360f)]
    public float pitchIntensity;

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
        if (this.transform.position.y >= _lastHeigth + 100)
        {
            _lastHeigth = _lastHeigth + 100;
            Debug.Log("Altitude : " + _lastHeigth);
        }
        
        Vector3 lift = new Vector3(0, 0.5f * liftCoeff * _airDensity * Mathf.Pow(_rb.velocity.x, 2), 0);
        _rb.AddForce(lift);

        Vector3 drag = -0.5f * dragCoeff * _airDensity * Mathf.Pow(_rb.velocity.x, 2) * _rb.velocity.normalized;
        _rb.AddForce(drag);

        Vector3 thrust = thrustPower * thrustCoeff * this.transform.right;
        _rb.AddForce(thrust);

        if (Input.GetButton("Roll"))
        {
            this.transform.Rotate(Vector3.forward * Time.deltaTime* rollIntensity * Input.GetAxis("Roll")); //On doit l'enlever à long terme
            //this.transform.Rotate(Vector3.right * Time.deltaTime * rollIntensity * -Input.GetAxis("Roll")); // C'est ça qui doit nous faire tourner à long terme
        }

        if (Input.GetButton("Pitch"))
        {
            this.transform.Rotate(Vector3.up * Time.deltaTime * pitchIntensity * Input.GetAxis("Pitch"));
        }
    }
}

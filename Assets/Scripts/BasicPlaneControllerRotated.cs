using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicPlaneControllerRotated : MonoBehaviour
{

    [Range(0f, 100f)]
    static public float WingArea;

    [Range(0f, 100f)]
    static public float LiftCoeff;

    [Range(0f, 100f)]
    static public float DragCoeff;

    [Range(0f, 100f)]
    static public float ThrustPower;

    public float ThrustCoeff;

    [Range(0f, 360f)]
    static public float RollIntensity;

    [Range(0f, 360f)]
    static public float PitchIntensity;

    [Range(0f, 360f)]
    static public float YawIntensity;

    private Rigidbody _rb;
    private float _airDensity = 1.184f;
    private int _lastHeigth;

    private Vector3 _thrust;
    private Vector3 _lift;
    private Vector3 _drag;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _lastHeigth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("SettingsUI");
        }
        
        if (transform.position.y >= _lastHeigth + 100)
        {
            _lastHeigth = _lastHeigth + 100;
            Debug.Log("Altitude : " + _lastHeigth);
        }

        //float dynamicLiftCoeff = LiftCoeff * (2-Vector3.Dot(_rb.velocity.normalized, transform.right));
        float dynamicLiftCoeff = LiftCoeff * transform.up.y;

        _lift = 0.5f * dynamicLiftCoeff * _airDensity * _rb.velocity.sqrMagnitude * transform.up;
        _drag = -0.5f * DragCoeff * _airDensity * _rb.velocity.sqrMagnitude * _rb.velocity.normalized;
        _thrust = ThrustPower * ThrustCoeff * transform.right;

        transform.Rotate(Vector3.right * Time.deltaTime * RollIntensity * -Input.GetAxis("Roll"));

        transform.Rotate(Vector3.forward * Time.deltaTime * PitchIntensity * Input.GetAxis("Pitch"));

        transform.Rotate(Vector3.up * Time.deltaTime * YawIntensity * Input.GetAxis("Yaw"));

        _thrust = _thrust * Input.GetAxis("Accelerate");

        _rb.AddForce(_lift);
        _rb.AddForce(_drag);
        _rb.AddForce(_thrust);
    }

    private void OnDrawGizmos()
    {
        Vector3 position = transform.position;
        Gizmos.DrawLine(position, position + _thrust / 1000);
        Gizmos.DrawLine(position, position + _lift / 1000);
        Gizmos.DrawLine(position, position + _drag / 1000);
    }
}

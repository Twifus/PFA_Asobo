using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneController : MonoBehaviour
{

    [Range(0f, 100f)]
    static public float WingArea = 5.0f;

    [Range(0f, 100f)]
    static public float LiftCoeff = 10.0f;

    [Range(0f, 100f)]
    static public float DragCoeff = 20.0f;

    [Range(0f, 100f)]
    static public float ThrustPower = 50.0f;

    public float ThrustCoeff;

    [Range(0f, 360f)]
    static public float RollIntensity = 60.0f;

    [Range(0f, 360f)]
    static public float PitchIntensity = 60.0f;

    [Range(0f, 360f)]
    static public float YawIntensity = 60.0f;
    
    private Rigidbody _body;

    public GameObject LeftWing;
    public GameObject RightWing;
    public GameObject Tail;

    private float _airDensity = 1.184f;

    private Vector3 _thrust;
    private Vector3 _llift;
    private Vector3 _rlift;
    private Vector3 _drag;

    // Use this for initialization
    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("SettingsUI");
        }

        // Dynamic lift coeff, depends on plane orientation
        float DynamicLiftCoeff = 5
            * Vector3.Dot(Vector3.Cross(transform.forward, _body.velocity.normalized), transform.right)
            * LiftCoeff;
        // Lift Formula
        _llift = 0.5f * DynamicLiftCoeff * _airDensity * _body.velocity.sqrMagnitude * transform.up;
        // Lift Modification 
        _rlift = _llift;

        _drag = -0.5f * DragCoeff * _airDensity * _body.velocity.sqrMagnitude * _body.velocity.normalized;

        _thrust = ThrustPower * ThrustCoeff * transform.forward;

        transform.Rotate(Vector3.forward * Time.deltaTime * RollIntensity * -Input.GetAxis("Roll"));

        transform.Rotate(Vector3.right * Time.deltaTime * PitchIntensity * -Input.GetAxis("Pitch"));

        transform.Rotate(Vector3.up * Time.deltaTime * YawIntensity * Input.GetAxis("Yaw"));

        _thrust = _thrust * Input.GetAxis("Accelerate");

        _body.AddForce(_llift);
        //_body.AddForceAtPosition(_llift, LeftWing.transform.position);
        //_body.AddForceAtPosition(_rlift, RightWing.transform.position);
        //_body.AddForceAtPosition(_llift, Tail.transform.position);
        _body.AddForce(_drag);
        _body.AddForce(_thrust);
    }

    private void OnDrawGizmos()
    {
        Vector3 bodyPos = transform.position;
        Vector3 lwPos = LeftWing.transform.position;
        Vector3 rwPos = RightWing.transform.position;

        Gizmos.DrawLine(bodyPos, bodyPos + _thrust / 1000);
        //Gizmos.DrawLine(bodyPos, bodyPos + _llift / 1000);
        Gizmos.DrawLine(lwPos, lwPos + _llift / 1000);
        Gizmos.DrawLine(rwPos, rwPos + _rlift / 1000);
        Gizmos.DrawLine(bodyPos, bodyPos + _drag / 1000);
    }
}

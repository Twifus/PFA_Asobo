using System.Collections;
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

    public GameObject[] PathRenderers;

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
        Vector3 baseLift = 0.5f * PlaneSettings.LiftCoeff * PlaneSettings.AirDensity * _body.velocity.sqrMagnitude * transform.up;

        _llift = (CustomInput.GetAxis("Pitch") + PlaneSettings.RollIntensity * CustomInput.GetAxis("Roll")) * baseLift;
        _rlift = (CustomInput.GetAxis("Pitch") - PlaneSettings.RollIntensity * CustomInput.GetAxis("Roll")) * baseLift;

        /* Drag */
        _drag = -0.5f * PlaneSettings.DragCoeff * PlaneSettings.AirDensity * _body.velocity.sqrMagnitude * _body.velocity.normalized;

        /* Thrust */
        _thrust = PlaneSettings.ThrustPower * PlaneSettings.ThrustCoeff * transform.forward;
        _thrust = _thrust * CustomInput.GetAxis("Accelerate");
        
        _body.AddForceAtPosition(_thrust, Engine.position);
        _body.AddForceAtPosition(_llift, LeftWing.position);
        _body.AddForceAtPosition(_rlift, RightWing.position);
        _body.AddForce(_drag);

        _body.AddForceAtPosition(- 0.1f * baseLift.magnitude * CustomInput.GetAxis("Yaw") * transform.right, Tail.position); // Yaw
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

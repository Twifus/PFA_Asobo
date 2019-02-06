using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTestController : MonoBehaviour {

    int mode = 0;
    Vector3 pos;
    Quaternion rot;

    Vector3 px;
    Vector3 nx;

    Vector3 pz;
    Vector3 nz;

	// Use this for initialization
	void Start () {
        pos = transform.position;
        rot = transform.rotation;
	}
	
    float getPitch()
    {
        px = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        nx = Vector3.Cross(px, Vector3.up);
        float angle = Vector3.SignedAngle(px, transform.forward, nx);

        Vector3 reference;

        if (Mathf.Abs(transform.up.y) < 0.001f)
            if (Mathf.Abs(transform.up.y) > Mathf.Abs(transform.right.y))
                reference = transform.up;
            else
                reference = transform.right;
        else
            reference = transform.up;

        if (reference.y < 0)
            angle = 180f - angle;

        if (angle < 0)
            angle = 360 + angle;

        return angle;
    }

    float getRoll()
    {
        pz = Vector3.ProjectOnPlane(transform.right, Vector3.up);
        nz = Vector3.Cross(pz, Vector3.up);
        float angle = Vector3.SignedAngle(pz, transform.right, nz);

        Vector3 reference;

        if (Mathf.Abs(transform.up.y) < 0.001f)
            if (Mathf.Abs(transform.up.y) > Mathf.Abs(transform.forward.y))
                reference = transform.up;
            else
                reference = transform.forward;
        else
            reference = transform.up;

        if (reference.y < 0)
            angle = 180f - angle;

        if (angle < 0)
            angle = 360 + angle;

        return angle;
    }

    Vector3 getAngles()
    {
        Vector3 result = transform.eulerAngles;
        
        return result;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            mode = (mode + 1) % 3;
        }

        Debug.Log(mode + " " + getAngles());

        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            mode = 0;
            transform.position = pos;
            transform.rotation = rot;
        }

        if (mode == 0)
        {
            transform.RotateAround(transform.position, transform.right, Input.GetAxis("Roll"));
        }

        if (mode == 1)
        {
            transform.RotateAround(transform.position, transform.forward, Input.GetAxis("Roll"));
        }

        if (mode == 2)
        {
            transform.RotateAround(transform.position, transform.up, Input.GetAxis("Roll"));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + px*10);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + nx*10);
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + pz * 10);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + nz * 10);
    }

}

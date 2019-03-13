using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : IPlane {

    private static Dictionary<GameObject, Plane> listInstances = new Dictionary<GameObject, Plane>();
    private static readonly object padlock = new object();

    #region Variables
    
    private GameObject _plane;
    private Rigidbody _rigidbody;

    #endregion

    #region Properties

    public Rigidbody Rigidbody {
        get {
            return _rigidbody;
        }
    }

    public Vector3 Position {
        get {
            return _plane.GetComponent<Transform>().position;
        }
    }

    public Quaternion Rotation {
        get {
            return _plane.GetComponent<Transform>().rotation;
        }
    }

    public float Pitch {
        get {
            return _Pitch();
        }
    }

    public float Roll {
        get {
            return _Roll();
        }
    }

    public float Yaw {
        get {
            return _Yaw();
        }
    }

    /* IPlane */
    public float posX { get { return _rigidbody.transform.position.x; } }
    public float posY { get { return _rigidbody.transform.position.y; } }
    public float posZ { get { return _rigidbody.transform.position.z; } }

    public float rotationX { get { return _rigidbody.transform.rotation.x; } }
    public float rotationY { get { return _rigidbody.transform.rotation.y; } }
    public float rotationZ { get { return _rigidbody.transform.rotation.z; } }
    public float rotationW { get { return _rigidbody.transform.rotation.w; } }

    // System.Numerics n'est pas compatible dans cette version
    // vector3 up { get; }
    // vector3 forward { get; }
    // vector3 right { get; }

    public float speedX { get { return _rigidbody.velocity.x; } }
    public float speedY { get { return _rigidbody.velocity.y; } }
    public float speedZ { get { return _rigidbody.velocity.z; } }
    public float speedMagnitude { get { return _rigidbody.velocity.magnitude; } }

    //public float accelerationX { get; }
    //public float accelerationY { get; }
    //public float accelerationZ { get; }
    //public float accelerationMagnitude { get; }

    #endregion


    #region Constructor

    private Plane(GameObject plane) {
        _plane = plane;

        _rigidbody = plane.GetComponent<Rigidbody>();
    }

    #endregion


    #region Public Methods

    public void AddForce(Vector3 force) {
        _rigidbody.AddForce(force);
    }

    public static Plane NewPlane(GameObject plane) {
        lock (padlock) { // thread safety
            if (listInstances.ContainsKey(plane)) {
                return listInstances[plane];
            }
            else {
                Plane ret = new Plane(plane);
                listInstances.Add(plane, ret);
                return ret;
            }
        }
    }

    #endregion

    #region Private Methods

    private float _Pitch() {
        Vector3 planeForward = _rigidbody.transform.forward;
        Vector3 vectorOnPlane = Vector3.ProjectOnPlane(planeForward, Vector3.up);
        Vector3 referenceAxis = Vector3.Cross(vectorOnPlane, Vector3.up);

        //if (Vector3.Dot(Vector3.up, _rigidbody.transform.up) > 0) {
        //    vectorOnPlane = Vector3.ProjectOnPlane(planeForward, Vector3.up);
        //}
        //else { // Plane is upside down
        //    vectorOnPlane = Vector3.ProjectOnPlane(-planeForward, Vector3.up);
        //}

        float angle = Vector3.SignedAngle(vectorOnPlane, planeForward, referenceAxis);
        return angle;
    }

    private float _Roll() {
        Vector3 planeForward = _rigidbody.transform.forward;
        Vector3 planeRight = _rigidbody.transform.right;
        Vector3 vectorOnPlane = Vector3.ProjectOnPlane(planeForward, Vector3.up);
        Vector3 vectorRef = Vector3.Cross(Vector3.up, vectorOnPlane);

        float angle = Vector3.SignedAngle(planeRight, vectorRef, _rigidbody.transform.forward);
        return angle;
    }

    private float _Yaw() {
        Vector3 forwardOnPlane = Vector3.ProjectOnPlane(_rigidbody.transform.forward, Vector3.up);
        Vector3 north = Vector3.forward;

        float angle = Vector3.SignedAngle(north, forwardOnPlane, Vector3.up);
        return angle;
    }

    #endregion
}

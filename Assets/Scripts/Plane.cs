using System.Collections;
using System.Collections.Generic;
using s = System.Numerics;
using UnityEngine;

public class Plane : IFlyingObject {

    private static Dictionary<GameObject, Plane> listInstances = new Dictionary<GameObject, Plane>();
    private static readonly object padlock = new object();

    #region Variables
    
    private GameObject _plane;
    private Rigidbody _rigidbody;

    #endregion

    #region Properties

    /* IFLYING OBJECT */

    public s.Vector3 pos { get { return UnityVector3ToSystemVector3(_rigidbody.transform.position); } }

    public s.Quaternion rotation { get { return UnityQuaternionToSystemQuaternion(_rigidbody.transform.rotation); } }

    public float roll { get { return _Roll(); } }
    public float pitch { get { return _Pitch(); } }
    public float yaw { get { return _Yaw(); } }

    public s.Vector3 up { get { return UnityVector3ToSystemVector3(_rigidbody.transform.up); } }
    public s.Vector3 forward { get { return UnityVector3ToSystemVector3(_rigidbody.transform.forward); } }
    public s.Vector3 right { get { return UnityVector3ToSystemVector3(_rigidbody.transform.right); } }

    public s.Vector3 speed { get { return UnityVector3ToSystemVector3(_rigidbody.velocity); } }

    public float time { get { return Time.time; } }
    
    /* PLANE */

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

    private s.Vector3 UnityVector3ToSystemVector3(Vector3 vector) {
        return new s.Vector3(vector.x, vector.y, vector.z);
    }

    private s.Quaternion UnityQuaternionToSystemQuaternion(Quaternion quaternion) {
        return new s.Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
    }

    #endregion
}

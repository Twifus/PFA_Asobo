using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Accesseur standardisé à la trajectoire d'un avion
/// </summary>
public class Plane : IFlyingObject {

    /// <summary>
    /// Ensemble des instances de Plane existantes
    /// </summary>
    private static Dictionary<GameObject, Plane> listInstances = new Dictionary<GameObject, Plane>();

    /// <summary>
    /// Lock utilisé pour sécuriser les accès concurrents à listInstances
    /// </summary>
    private static readonly object padlock = new object();

    #region Variables
    
    /// <summary>
    /// GameObject associé à l'instance
    /// </summary>
    private GameObject _plane;

    /// <summary>
    /// RigidBody associé à l'instance
    /// </summary>
    private Rigidbody _rigidbody;

    #endregion

    #region Properties

    /* IFLYING OBJECT */

    public System.Numerics.Vector3 pos { get { return UnityVector3ToSystemVector3(_rigidbody.transform.position); } }

    public System.Numerics.Quaternion rotation { get { return UnityQuaternionToSystemQuaternion(_rigidbody.transform.rotation); } }

    public float roll { get { return _Roll(); } }
    public float pitch { get { return _Pitch(); } }
    public float yaw { get { return _Yaw(); } }

    public float rollScalar { get { return _RollScalar(); } }
    public float pitchScalar { get { return _PitchScalar(); } }
    public float yawScalar { get { return _YawScalar(); } }

    public System.Numerics.Vector3 up { get { return UnityVector3ToSystemVector3(_rigidbody.transform.up); } }
    public System.Numerics.Vector3 forward { get { return UnityVector3ToSystemVector3(_rigidbody.transform.forward); } }
    public System.Numerics.Vector3 right { get { return UnityVector3ToSystemVector3(_rigidbody.transform.right); } }

    public System.Numerics.Vector3 speed { get { return UnityVector3ToSystemVector3(_rigidbody.velocity); } }

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

    /// <summary>
    /// Donne une instance de Plane associé à un GameObject donné
    /// </summary>
    // <param name="plane">GameObject à associer au Plane</param>
    /// <returns>
    /// Si aucune instance liée à GameObject n'existe, retourne une nouvelle instance de Plane.
    /// Sinon, retourne l'instance correspondante.
    /// </returns>
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

    private float _PitchScalar() {
        Vector3 planeForward = _rigidbody.transform.forward;
        //Vector3 vectorOnPlane = Vector3.ProjectOnPlane(planeForward, Vector3.up);
        //Vector3 referenceAxis = Vector3.Cross(vectorOnPlane, Vector3.up);

        float scalar = Vector3.Dot(Vector3.up, planeForward);
        return scalar;
    }

    private float _Roll() {
        Vector3 planeForward = _rigidbody.transform.forward;
        Vector3 planeRight = _rigidbody.transform.right;
        Vector3 vectorOnPlane = Vector3.ProjectOnPlane(planeForward, Vector3.up);
        Vector3 vectorRef = Vector3.Cross(Vector3.up, vectorOnPlane);

        float angle = Vector3.SignedAngle(planeRight, vectorRef, _rigidbody.transform.forward);
        return angle;
    }

    private float _RollScalar() {
        Vector3 planeForward = _rigidbody.transform.forward;
        Vector3 planeRight = _rigidbody.transform.right;
        Vector3 vectorOnPlane = Vector3.ProjectOnPlane(planeForward, Vector3.up);
        Vector3 vectorRef = Vector3.Cross(Vector3.up, vectorOnPlane);

        float scalar = Vector3.Dot(planeRight, vectorRef);
        return scalar;
    }

    private float _Yaw() {
        Vector3 forwardOnPlane = Vector3.ProjectOnPlane(_rigidbody.transform.forward, Vector3.up);
        Vector3 north = Vector3.forward;

        float angle = Vector3.SignedAngle(north, forwardOnPlane, Vector3.up);
        return angle;
    }

    private float _YawScalar() {
        Vector3 forwardOnPlane = Vector3.ProjectOnPlane(_rigidbody.transform.forward, Vector3.up);
        Vector3 north = Vector3.forward;

        float scalar = Vector3.Dot(north, forwardOnPlane);
        return scalar;
    }

    private System.Numerics.Vector3 UnityVector3ToSystemVector3(Vector3 vector) {
        return new System.Numerics.Vector3(vector.x, vector.y, vector.z);
    }

    private System.Numerics.Quaternion UnityQuaternionToSystemQuaternion(Quaternion quaternion) {
        return new System.Numerics.Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
    }

    #endregion
}

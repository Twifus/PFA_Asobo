using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane {

    private static Dictionary<GameObject, Plane> listInstances = new Dictionary<GameObject, Plane>();
    private static readonly object padlock = new object();

    #region Variables

    //private float _wingArea;
    //private float _liftCoeff;
    //private float _dragCoeff;
    //private float _trustPower;
    //private float _trustCoeff;
    //private float _rollIntensity;
    //private float _pitchIntensity;
    //private float _yawIntensity;

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

    public float WingArea {
        get {
            //return _wingArea;
            return PlaneSettings.WingArea;
        }
    }

    public float LiftCoeff {
        get {
            //return _liftCoeff;
            return PlaneSettings.LiftCoeff;
        }
    }

    public float DragCoeff {
        get {
            //return _dragCoeff;
            return PlaneSettings.DragCoeff;
        }
    }

    public float TrustPower {
        get {
            //return _trustPower;
            return PlaneSettings.ThrustPower;
        }
    }

    public float TrustCoeff {
        get {
            //return _trustCoeff;
            return PlaneSettings.ThrustCoeff;
        }
    }

    public float RollIntensity {
        get {
            //return _rollIntensity;
            return PlaneSettings.RollIntensity;
        }
    }

    public float PitchIntensity {
        get {
            //return _pitchIntensity;
            return PlaneSettings.PitchIntensity;
        }
    }

    public float YawIntensity {
        get {
            //return _yawIntensity;
            return PlaneSettings.YawIntensity;
        }
    }

    #endregion


    #region Constructor

    private Plane(GameObject plane) {
        _plane = plane;

        _rigidbody = plane.GetComponent<Rigidbody>();
        LoadSettings();
    }

    #endregion


    #region Public Methods

    public void AddForce(Vector3 force) {
        _rigidbody.AddForce(force);
    }


    public void LoadSettings() {
        //_wingArea = PlaneSettings.WingArea;
        //_liftCoeff = PlaneSettings.LiftCoeff;
        //_dragCoeff = PlaneSettings.DragCoeff;
        //_trustPower = PlaneSettings.ThrustPower;
        //_trustCoeff = PlaneSettings.ThrustCoeff;
        //_rollIntensity = PlaneSettings.RollIntensity;
        //_pitchIntensity = PlaneSettings.PitchIntensity;
        //_yawIntensity = PlaneSettings.YawIntensity;
    }

    public void LoadSettings(float wingArea, float liftCoeff, float dragCoeff,
                            float thrustPower, float thrustCoeff, float rollIntensity,
                            float pitchIntensity, float yawIntensity) {
        //_wingArea = wingArea;
        //_liftCoeff = liftCoeff;
        //_dragCoeff = dragCoeff;
        //_trustPower = thrustPower;
        //_trustCoeff = thrustCoeff;
        //_rollIntensity = rollIntensity;
        //_pitchIntensity = pitchIntensity;
        //_yawIntensity = yawIntensity;
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
        Vector3 planeRight = _rigidbody.transform.right;
        Vector3 vectorOnPlane = Vector3.ProjectOnPlane(planeRight, Vector3.up);

        // if (Vector3.Dot(Vector3.up, _rigidbody.transform.up) < 0) {
        //     vectorOnPlane = -vectorOnPlane;
        // }

        float angle = Vector3.SignedAngle(planeRight, vectorOnPlane, _rigidbody.transform.forward);
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

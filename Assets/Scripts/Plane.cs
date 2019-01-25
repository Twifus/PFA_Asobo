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
}

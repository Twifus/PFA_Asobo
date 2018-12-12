using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraController : MonoBehaviour {

    public GameObject target;
    Transform TargetRotation;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = target.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, target.transform.rotation, Time.deltaTime * 10);
        transform.LookAt(target.transform);
    }
}

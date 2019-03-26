using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCameraController : MonoBehaviour {

    public GameObject target;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * 20); 
        transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, Time.deltaTime * 15); 
    }
}

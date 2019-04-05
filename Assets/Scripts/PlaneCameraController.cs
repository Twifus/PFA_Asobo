using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controleur de caméra suivant une cible avec un retard
/// </summary>
public class PlaneCameraController : MonoBehaviour {

    /// <summary>
    /// GameObject ciblé par la caméra
    /// </summary>
    public GameObject target;
    
    void Start () {
    }
	
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * 20); 
        transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, Time.deltaTime * 15); 
    }
}

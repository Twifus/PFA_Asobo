using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingPoint : MonoBehaviour {

    //BoxCollider Cld;


	// Use this for initialization
	void Start () {
        //Cld = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("1 point");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingPoint : MonoBehaviour {

    AudioSource Point;


	// Use this for initialization
	void Start () {
        Point = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        Point.Play(0);
        Debug.Log("1 point");
    }
}

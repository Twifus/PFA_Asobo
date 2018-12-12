using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingPoint : MonoBehaviour {

    private AudioSource _point;


	// Use this for initialization
	void Start () {
        _point = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.right * Time.deltaTime * 25);

	}

    private void OnTriggerEnter(Collider other)
    {
        _point.Play(0);
        Debug.Log("1 point");
    }
}

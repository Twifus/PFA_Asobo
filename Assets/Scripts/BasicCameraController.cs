﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraController : MonoBehaviour {

    public GameObject target;
    private Vector3 _offset;

	// Use this for initialization
	void Start () {
		_offset = this.transform.position - target.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = target.transform.position + _offset;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        //Debug.Log("triggering");
        GetComponentInParent<Circuit>().OnTriggerEnterChild(gameObject);
    }
}

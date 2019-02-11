using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circuit : MonoBehaviour {

    private List<GameObject> rings;
    private GameObject end;
    private int goal;
    private int current;

	void Start () {
		rings = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ring"));
        end = GameObject.FindGameObjectWithTag("End");
        goal = rings.Count;
        current = 0;
    }

    public void OnTriggerEnterChild(GameObject other) {
        if (other == end) {
            if (current == goal) {
                Debug.Log("Finish !!!");
            }
        }
        if (rings.Contains(other)) {
            rings.Remove(other);
            current++;
            //Debug.Log("Ring passed !");
        }
    }
}

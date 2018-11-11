using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlaneController : MonoBehaviour {

    [Range(0f, 100f)]
    public float wing_area;

    [Range(0f, 100f)]
    public float lift_coeff;

    [Range(0f, 100f)]
    public float drag_coeff;

    [Range(0f, 100f)]
    public float thrust_power;
    
    public float thrust_coeff;

    private Rigidbody rb;
    private float air_density = 1.184f;

    private int last_heigth;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        last_heigth = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y >= last_heigth + 100)
        {
            last_heigth = last_heigth + 100;
            Debug.Log("Altitude : " + last_heigth);
        }
        
        Vector3 lift = new Vector3(0, 0.5f * lift_coeff * air_density * Mathf.Pow(rb.velocity.x, 2), 0);
        rb.AddForce(lift);

        Vector3 drag = -0.5f * drag_coeff * air_density * Mathf.Pow(rb.velocity.x, 2) * rb.velocity.normalized;
        rb.AddForce(drag);

        Vector3 thrust = new Vector3(thrust_power * thrust_coeff, 0, 0);
        rb.AddForce(thrust);
	}
}

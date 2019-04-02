using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotation : MonoBehaviour
{
    [System.Serializable]
    public struct ZRot
    {
        public float min;
        public float max;
    }

    public float tolerance;
    public GameObject _outer;
    public GameObject _indicator;
    private int _outer_rot_side;
    private float _rotation;
    public ZRot rotRange;

    private AudioSource _point;

    void Start()
    {
        _outer_rot_side = Random.Range(0, 2) * 2 - 1; // -1 or 1
        _rotation = Random.Range(rotRange.min, rotRange.max);
        _indicator.transform.Rotate(Vector3.forward, _rotation);
        _point = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _outer.transform.Rotate(Vector3.right * _outer_rot_side * Time.deltaTime * 25);
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 reference = Vector3.ProjectOnPlane(other.transform.up, _indicator.transform.forward);
        if (Vector3.Angle(_indicator.transform.up, reference) < tolerance)
        { 
            Debug.Log("15 point");
            _point.Play(0);
            other.GetComponent<FigureManager>().UpdateScore(15);
        }
    }
}

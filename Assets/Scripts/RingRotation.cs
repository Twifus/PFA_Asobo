using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotation : MonoBehaviour
{
    private Quaternion _rotation;
    public float epsilon;
    public GameObject _bar;

    void Start()
    {
        _rotation = Quaternion.Euler(0, 0, 0);
        //_point = GetComponent<AudioSource>();
        _bar.transform.rotation = _rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * 25);
        _bar.transform.rotation = _rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.rotation.z);
        if (other.transform.rotation.z >= _rotation.z - epsilon && other.transform.rotation.z <= _rotation.z + epsilon)
        { 
            //_point.Play(0);
            Debug.Log("15 point");
            other.GetComponent<FigureManager>().UpdateScore(15);
        }
    }
}

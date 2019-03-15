using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotation : MonoBehaviour
{
    private Quaternion _rotation;
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
        if (other.transform.rotation == _rotation)
        { 
            //_point.Play(0);
            Debug.Log("15 point");
            other.GetComponent<FigureManager>().UpdateScore(15);
        }
    }
}

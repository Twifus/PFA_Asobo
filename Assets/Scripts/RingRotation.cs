using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotation : MonoBehaviour
{
    private Quaternion _rotation;
    public float epsilon;
    public GameObject _bar;

    [System.Serializable]
    public struct ZRot
    {
        public float min;
        public float max;
    }

    public ZRot rotRange;

    void Start()
    {
        _rotation = Quaternion.Euler(0, 0, Random.Range(rotRange.min, rotRange.max));
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

        if (other.transform.rotation.z >= _rotation.z - epsilon && other.transform.rotation.z <= _rotation.z + epsilon)
        { 
            Debug.Log("15 point");
            other.GetComponent<FigureManager>().UpdateScore(15);
        }
    }
}

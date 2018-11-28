using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public float SphereRadius = 5f;
    public float SmoothTime = 1f;

    private Vector3 _nextPointOnSphere;
    private float _interpolationFactor;
    private Vector3 _pointOnSphere;

    void Awake()
    {
        Random.InitState(666);
        _nextPointOnSphere = SphereRadius * Random.onUnitSphere;
        _nextPointOnSphere.y = Mathf.Abs(_nextPointOnSphere.y);
        _nextPointOnSphere.z = -Mathf.Abs(_nextPointOnSphere.z);
        _interpolationFactor = 1f;
    }

    void Update()
    {
        Move();
        transform.LookAt(Target);
    }

    void Move()
    {
        _interpolationFactor += SmoothTime * Time.deltaTime;
        if (_interpolationFactor >= 0.99f)
        {
            _pointOnSphere = _nextPointOnSphere;
            _nextPointOnSphere = SphereRadius * Random.onUnitSphere;
            _nextPointOnSphere.y = Mathf.Abs(_nextPointOnSphere.y);
            _nextPointOnSphere.z = -Mathf.Abs(_nextPointOnSphere.z);
            _interpolationFactor = 0f;
        }
        transform.position = Target.transform.position + Vector3.Slerp(_pointOnSphere, _nextPointOnSphere, _interpolationFactor);
    }
}

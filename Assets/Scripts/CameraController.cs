using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public int DistanceFromTarget = 1;
    public float HorizontalStickSensitivity = 10f;
    public float VerticalStickSensitivity = 10f;

    private Vector3 _posTargetSpace;
    private bool _lockCameraStorage = false;
    private bool _lockCamera
    {
        get
        {
            return _lockCameraStorage;
        }
        set
        {
            _lockCameraStorage = value;
            if (_lockCameraStorage)
                _posTargetSpace = Target.InverseTransformPoint(transform.position);
        }
    }
    private Vector3 _targetLastPos;

    void Awake()
    {
        InitMoveStick();
    }

    void Start()
    {
        _targetLastPos = Target.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            _lockCamera = !_lockCamera;
        }
        if (_lockCamera)
            FixPosition();
        else
            MoveStick();
    }

    private void InitMoveStick()
    {
        transform.position = Target.transform.position - DistanceFromTarget * Target.transform.right + 0.15f * Target.transform.up;
    }

    private void MoveStick()
    {
        transform.position += Target.transform.position - _targetLastPos;
        float x = Input.GetAxis("CameraHorizontal");
        float y = Input.GetAxis("CameraVertical");
        transform.RotateAround(Target.transform.position, transform.right, y * VerticalStickSensitivity);
        transform.RotateAround(Target.transform.position, transform.up, x * HorizontalStickSensitivity);
        transform.LookAt(Target, Target.transform.up);
    }

    private void FixPosition()
    {
        transform.position = Target.TransformPoint(_posTargetSpace);
        transform.LookAt(Target, Target.transform.up);
    }

    void LateUpdate()
    {
        _targetLastPos = Target.transform.position;
    }
}

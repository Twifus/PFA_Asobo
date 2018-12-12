using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public float HorizontalStickSensitivity = 10f;
    public float VerticalStickSensitivity = 10f;

    private bool _lockCamera;
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
        transform.position += Target.transform.position - _targetLastPos;
        _targetLastPos = Target.transform.position;
        if (Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            _lockCamera = !_lockCamera;
        }
        if (_lockCamera)
            CenterBehind();
        else
            MoveStick();
    }

    private void InitMoveStick()
    {
        CenterBehind();
    }

    private void MoveStick()
    {
        float x = Input.GetAxis("CameraHorizontal");
        float y = Input.GetAxis("CameraVertical");
        transform.RotateAround(Target.transform.position, transform.right, y * VerticalStickSensitivity);
        transform.RotateAround(Target.transform.position, transform.up, x * HorizontalStickSensitivity);
        transform.LookAt(Target);
    }

    private void CenterBehind()
    {
        transform.position = Target.transform.position - Target.transform.right + 0.15f * Target.transform.up;
        transform.LookAt(Target);
    }
}

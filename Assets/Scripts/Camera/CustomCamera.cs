using UnityEngine;


public class CustomCamera : MonoBehaviour
{
    [Header("Dumping")]
    public float DumpingSpeed;
    public float DumpingForce;
    [HideInInspector]
    public float StartDumpingForce;
    [HideInInspector]
    public Player Player;
    public static CustomCamera Instance;

    [HideInInspector]
    public float Width, Height;
    public Vector2 CameraArea;
    public Border Border;

    private Vector3 _dumpingDirection;
    private Vector3 _dumpingTarget;
    private Vector3 _startPosition;
    private Camera _camera;


    void Start()
    {
        SetInstance();
        _camera = GetComponent<Camera>();
        Width = PixelToUnit(Screen.width);
        Height = PixelToUnit(Screen.height);
        StartDumpingForce = DumpingForce;
        Player = FindObjectOfType<Player>();
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _dumpingDirection = Player.GetPlayerMovementDirection();
        Dumping(GetPlayerPosition());
        transform.position = GetLimitPosition();
    }

    private void Dumping(Vector3 targetPosition)
    {
        _dumpingTarget = targetPosition + _dumpingDirection * DumpingForce;
        transform.position = Vector3.Lerp(transform.position, _dumpingTarget, DumpingSpeed * Time.deltaTime);
    }

    private Vector3 GetDirection(Vector3 from, Vector3 target)
    {
        return (from - target).normalized;
    }
    private Vector3 GetPlayerPosition()
    {
        return Player.transform.position;
    }
    private Vector3 GetLimitPosition()
    {
        return new Vector3(Mathf.Clamp(transform.position.x, Border.LeftBottom.x + Width/2, Border.RightBottom.x - Width/2),
            Mathf.Clamp(transform.position.y, Border.LeftBottom.y + Height/2, Border.RightUpper.y - Height/2), _startPosition.z);
    }

    public float PixelToUnit(float pixel)
    {
        return (pixel * _camera.orthographicSize * 2) / _camera.pixelHeight;
    }
    private void SetInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}

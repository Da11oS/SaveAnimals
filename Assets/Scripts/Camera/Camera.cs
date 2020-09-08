using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float DumpingSpeed;
    public Player Player;
    public float DumpingForce;
    public static float Width
    {
        get
        {
            return Screen.width * 0.00593f;
        }
    }
    public static float Height
    {
        get
        {
            return Screen.height * 0.00593f;
        }
    }
    private Vector3 _playerPosition;
    private Vector3 _dumpingDirection;
    private Vector3 _dumpingTarget;
    private Vector3 _startPosition;
    [SerializeField]
    public float MaxHeight;
    private Grid _grid;
    void Start()
    {
        Player = FindObjectOfType<Player>();
        _playerPosition = Player.transform.position;
        _grid = FindObjectOfType<Grid>();
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _playerPosition = Player.transform.position;
        _dumpingDirection = Player.GetPlayerMovementDirection();
        Dumping();
    }

    private void Dumping()
    {
        _dumpingTarget = _playerPosition + _dumpingDirection * DumpingForce;
        float _dampingY = Mathf.Clamp(_dumpingTarget.y, _grid.transform.position.y - 2 * _grid.cellSize.y + Height / 2, MaxHeight);
        transform.position = Vector3.Lerp(transform.position, new Vector3(_dumpingTarget.x, _dampingY, _startPosition.z), DumpingSpeed * Time.deltaTime);
    }

    private Vector3 GetDirection(Vector3 from, Vector3 target)
    {
        return (from - target).normalized;
    }
}

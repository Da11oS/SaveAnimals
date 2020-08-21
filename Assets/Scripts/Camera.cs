using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float DumpingSpeed;
    public Player Player;
    public Vector3 DumpingForce;

    private Vector3 _playerPosition;
    private Vector3 _dumpingDirection;
    private Vector3 _dumpingTarget;
    [SerializeField]
    private float _maxHeight;
    private float _positionZ;
    private Grid _grid;
    void Start()
    {
        Player = FindObjectOfType<Player>();
        _playerPosition = Player.transform.position;
        _dumpingTarget = _playerPosition;
        _grid = FindObjectOfType<Grid>();
        _positionZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
            _playerPosition = Player.transform.position;
             DumpingForce = Player.GetRunDirection();
            Dumping();
    }

    private void Dumping()
    {
        _dumpingTarget = _playerPosition; //+ DumpingForce;
        float _dampingY = Mathf.Clamp(_dumpingTarget.y, _grid.transform.position.y - _grid.cellSize.y/2 + Screen.height / 2000, _maxHeight);
        transform.position = Vector3.Lerp(transform.position, new Vector3(_dumpingTarget.x, _dampingY, _positionZ), DumpingSpeed * Time.deltaTime);
    }

    private Vector3 GetDirection(Vector3 from, Vector3 target)
    {
        return (from - target).normalized;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float LerpSpeed;
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _impulse;
    [SerializeField] private float _groundRadius;
    [SerializeField] private float _rotationLerpSpeed;
    [SerializeField] private LayerMask _groundMask;
    private Vector3 _lastFramePosition;
    public Vector2 RunDirection
    {
        get; private set;
    }

    private void Start()
    {
        if(GetComponent<Rigidbody2D>()!=null)
        _rigidbody = GetComponent<Rigidbody2D>();
        else _rigidbody = gameObject.AddComponent<Rigidbody2D>();
        RunDirection = Vector3.right;
        _lastFramePosition = transform.position;
    }

    private void FixedUpdate()
    {
        Run(RunDirection);
        LerpRotation(new Quaternion(0, 0, 0, 1));
        _lastFramePosition = transform.position;
    }
    public Vector2 GetPlayerMovementDirection()
    {
        return transform.position - _lastFramePosition;
    }
    public void Run(Vector3 direction)
    {
        
       // transform.Translate(direction * _speed * Time.deltaTime);
        _rigidbody.velocity = new Vector2(direction.x * _speed * Time.deltaTime, _rigidbody.velocity.y);
    }
    public void Jump(Vector2 direction)
    {
        if (IsGround())
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(direction * _impulse, ForceMode2D.Impulse);
        }
    }
    public void LerpRunDirection(Vector2 to)
    {
        RunDirection =  Vector2.Lerp(RunDirection, to, LerpSpeed);
    }
    public void LerpRotation(Quaternion to)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, to, _rotationLerpSpeed * Time.deltaTime);
    }
    public Vector3 GetRunDirection()
    {
        return RunDirection;
    }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!MoveButton.IsClick)
        {
            LerpRunDirection(Vector3.right);
        }
    }

    private bool IsGround()
    {
        return Physics2D.OverlapCircle(transform.position + Vector3.down, _groundRadius, _groundMask);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float LerpSpeed;
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _impulse;
    [SerializeField]
    private float _groundRadius;
    [SerializeField]
    private float _rotationLerpSpeed;
    [SerializeField]
    private LayerMask _groundMask;
    private Vector2 _runDirection;
    
    void Start()
    {
        if(GetComponent<Rigidbody2D>()!=null)
        _rigidbody = GetComponent<Rigidbody2D>();
        else _rigidbody = gameObject.AddComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Run(_runDirection);
    }
    // Update is called once per frame
    public void Run(Vector3 direction)
    {
        transform.Translate(direction * _speed * Time.deltaTime);
      //  _rigidbody.velocity = transform.right * _speed * Time.deltaTime;
    }
    public void Jump(Vector2 direction)
    {
        if (IsGround())
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(direction * _impulse, ForceMode2D.Impulse);
        }
    }
    public void LerpRunDirection(Vector2 to)
    {
        _runDirection =  Vector2.Lerp(_runDirection, to, LerpSpeed);
    }
    public void LerpRotation(Quaternion to)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, to, _rotationLerpSpeed * Time.deltaTime);
    }
    public Vector3 GetRunDirection()
    {
        return _runDirection;
    }
    private bool IsGround()
    {
        Debug.DrawRay(transform.position + Vector3.down - Vector3.right * 0.75f, Vector2.right);
       // return Physics2D.OverlapBox(transform.position + Vector3.down, new Vector2(1.5f,0.3f), _groundMask);
        return Physics2D.OverlapCircle(transform.position + Vector3.down, _groundRadius, _groundMask);
    }
}


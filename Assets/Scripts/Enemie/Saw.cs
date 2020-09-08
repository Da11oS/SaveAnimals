using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public Vector3 Distance;
    [SerializeField]private Vector2 _leftSide, _rightSide;
    private Rigidbody2D _rigidbody2D;
    [SerializeField]private float _speed;
    private Vector2 _target;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //  SetVelocity(Vector2.right);
        _leftSide = transform.position - Distance;
        _rightSide = transform.position + Distance;
        _target = _leftSide;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(IsSide(_leftSide))
        {
            _target = _rightSide;
        }
        else if(IsSide(_rightSide))
        {
            _target = _leftSide;
        }
        SetVelocity(_target);
    }
    private bool IsSide(Vector2 target)
    {
        return Vector2.Distance(transform.position, target) < 0.5f;
    }
    private void SetVelocity(Vector3 direction)
    {
        transform.position = Vector2.MoveTowards(transform.position, direction, _speed * Time.deltaTime);
    }
}

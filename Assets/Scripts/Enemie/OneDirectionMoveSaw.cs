using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDirectionMoveSaw : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 _direction;
    private Rigidbody2D _rigidbody;
    private float _speed;

    // Update is called once per frame
    void Update()
    {
        _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);
    }
}

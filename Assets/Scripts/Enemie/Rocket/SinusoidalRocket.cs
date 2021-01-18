using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strategy.Rocket;
public class SinusoidalRocket : RocketStrategy
{
    public float Radius;
    public float RotateSpeed;
    private float _rotation;
    override public void Move()
    {
        _rotation += RotateSpeed * Time.deltaTime;
        if (_rotation > 360)
        {
            _rotation = 0;
        }

        transform.position = new Vector3(transform.position.x + Direction.x * Speed * Time.deltaTime,
            StartPosition.y + Mathf.Sin(_rotation) * Radius, 0);
    }
    private void LookAt(Vector2 currentPosition, Vector2 nextPosition)
    {
        Vector2 direction = currentPosition - nextPosition;
        direction /= direction.magnitude;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotateSpeed * Time.deltaTime);
    }
}

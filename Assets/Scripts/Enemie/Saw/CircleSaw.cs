using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Strategy.Saw;

public class CircleSaw: SawStrategy
{
    public float Radius;
    private float _angle;
    public Vector3 Center { get => StartPosition; }

    public new void Start()
    {
        base.Start();
    }
    override public void Move()
    {
        Vector2 position;
        position.x = Center.x + Mathf.Cos(_angle) * Radius;
        position.y = Center.y + Mathf.Sin(_angle) * Radius;

        _angle += Time.deltaTime * Speed;

        if (_angle >= 360)
        {
            _angle = 0;
        }
        transform.position = position;
    }
    override public void Draw()
    {
        Gizmos.color = Color.yellow;

#if UNITY_EDITOR
        Gizmos.DrawLine(transform.position + Vector3.right * Radius, transform.position - Vector3.right * Radius);
        Gizmos.DrawLine(transform.position + Vector3.up * Radius, transform.position - Vector3.up * Radius);
#else
        Gizmos.DrawLine(Center + Vector3.right * Radius, Center - Vector3.right * Radius);
        Gizmos.DrawLine(Center + Vector3.up * Radius, Center - Vector3.up * Radius);
#endif
    }
}

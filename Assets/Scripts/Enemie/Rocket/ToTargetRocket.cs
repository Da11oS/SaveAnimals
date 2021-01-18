using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strategy.Rocket;

public class ToTargetRocket : RocketStrategy
{
    public Transform Target;

    public new void Start()
    {
        base.Start();
        Target = FindObjectOfType<Player>().transform;

    }
    public override void Move()
    {
        transform.position += GetDirection(Target.position, transform.position) * Speed * Time.deltaTime;
    }
    public Vector3 GetDirection(Vector3 target, Vector3 curentPosition)
    {
        var direction = target - curentPosition;
        return direction / direction.magnitude;
    }
}

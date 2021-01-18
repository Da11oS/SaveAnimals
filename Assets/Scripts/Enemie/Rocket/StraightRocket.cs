using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strategy.Rocket;

public class StraightRocket : RocketStrategy
{
   
    override public void Move()
    {
        transform.position += (Vector3)Direction * Speed * Time.deltaTime;
    }
}

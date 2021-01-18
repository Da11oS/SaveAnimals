
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;


namespace Strategy.Saw
{
    interface ISaw
    {
        void Move();
        void Draw();
    }

    abstract public class SawStrategy : MonoBehaviour,ISaw
    {
        public float Speed;
        public Vector3 StartPosition;

        public void Start()
        {
            StartPosition = transform.position;
        }
        public void OnDrawGizmos()
        {
            Draw();
        }
        public void Update()
        {
            Move();
        }
        abstract public void Move();
        abstract public void Draw();

    }
    public enum Movements
    {
        Path,
        Circle,
        Static
    };

}


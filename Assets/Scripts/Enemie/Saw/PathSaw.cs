using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using Strategy.Saw;

public class PathSaw: SawStrategy
{
    public Transform[] Targets;
    public MoveFormat Format;
    [HideInInspector]public int TargetCount;

    private bool _showFoldOut;
    private int _index = 0;
    private int _indexDirection;
    override public void Move()
    {
        Vector2 direction = Targets[_index].position - transform.position ;
        direction /= direction.magnitude;
        Vector2 position = (Vector2)transform.position + direction * Speed * Time.deltaTime;
        transform.position = position;
        if (Vector2.Distance(transform.position, Targets[_index].position) < 0.5f)
        {
            SetNewTarget();
        }
    }
    public new void Start()
    {
        base.Start();
        _indexDirection = 1;
    }
    private void SetNewTarget()
    {
        int index = _index;
        if (Format == MoveFormat.Open)
        {
            if (_index >= Targets.Length - 1)
            {
                _indexDirection = -1;
            }
            else if(_index <= 0)
            {
                _indexDirection = 1;
            }
             _index += _indexDirection;
        }
        else
        {
            if (_index >= Targets.Length - 1)
            {
                _index = 0;
            }
            else  ++_index;
        }
    }
    override public void Draw()
    {
        if (Format == MoveFormat.Open)
        {
            for (int i = 0; i < Targets.Length - 1; i++)
            {
                Gizmos.DrawLine(Targets[i].position, Targets[i + 1].position);
            }
        }
        if (Format == MoveFormat.Close)
        {
            for (int i = 0; i < Targets.Length - 1; i++)
            {
                Gizmos.DrawLine(Targets[i].position, Targets[i + 1].position);
            }
            Gizmos.DrawLine(Targets[Targets.Length - 1].position, Targets[0].position);
        }

    }
    public enum MoveFormat
    {
        Close, Open
    };
}

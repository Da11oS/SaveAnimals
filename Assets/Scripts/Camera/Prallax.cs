﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prallax : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private float _maxDistanceToCamera;
    [SerializeField] private float _parallaxSpeed;
    private Player _player;
    [SerializeField] private float _length;
    private Vector3 _lastFrameCameraPosition;
    private static int _backgroundsCount = 0;
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _camera = FindObjectOfType<Camera>();
        _length = GetComponent<SpriteRenderer>().bounds.size.x;

        _maxDistanceToCamera = _length + Camera.Width / 2;
        _lastFrameCameraPosition = _camera.transform.position;
        _backgroundsCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPassedCamera(transform))
        {
            SwapBackgrounds();
        }

        if (WasMove())
            Scroll();
       // transform.position = new Vector3(transform.position.x, _camera.transform.position.y, 0);
        _lastFrameCameraPosition = _camera.transform.position;
    }
    public void Scroll()
    {
        transform.Translate(-(_camera.transform.position - _lastFrameCameraPosition).normalized * _parallaxSpeed * Time.deltaTime);
    }
    public void SwapBackgrounds()
    {
        var newPosition = (Vector3)_player.RunDirection * _length;
        newPosition -= (Vector3)_player.RunDirection * 0.2f;
        transform.position = transform.position + _backgroundsCount * newPosition;
    }
    private bool IsPassedCamera(Transform child)
    {
        if (_player.RunDirection.x > 0)
        {
            return _camera.transform.position.x - child.position.x > _maxDistanceToCamera;
        }
        else return _camera.transform.position.x - child.position.x < -_maxDistanceToCamera;
    }
    private bool WasMove()
    {
        return Mathf.Abs(_camera.transform.position.x - _lastFrameCameraPosition.x) > 0.01f;
    }
}


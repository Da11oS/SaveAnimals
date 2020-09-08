﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstaclesGenerator : MonoBehaviour
{
    public Transform WarningTransform;
    public GameObject WarningInstance;

    [SerializeField] private float _time;
    [SerializeField] private GameObject _obstacle;
    [Range(0, 10)] [SerializeField]
    private float _redaxMinTime;
    [Range(10, 100)] [SerializeField]
    private float _redaxMaxTime;
    private float _reduxTime;
    [SerializeField]
    private float MaxPositionY, MinPositionY;
    private float _ScreenHeight;
    private Transform _playerTransform;
    private void Start()
    {
        _playerTransform = FindObjectOfType<Player>().transform;
        _reduxTime = _time + Random.Range(_redaxMinTime, _redaxMaxTime);
        StartCoroutine(Generate());
    }

    IEnumerator Generate()
    {
        int i = 0;
        while (true)
        {
            if (i >= 1)
            {
                _reduxTime = _time + Random.Range(_redaxMinTime, _redaxMaxTime);
                Vector3 position = new Vector3(transform.position.x, _playerTransform.position.y + Random.Range(MinPositionY, MaxPositionY), _obstacle.transform.position.z);
                Instantiate(_obstacle, position, _obstacle.transform.rotation);
            }
            i++;
            yield return new WaitForSeconds(_reduxTime);
        }
    }
}

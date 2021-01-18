using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strategy.Rocket;

public class ObstaclesGenerator : MonoBehaviour
{
    public Transform WarningTransform;
    public GameObject WarningInstance;
    public float Radius;
    public float Speed;
    public float Angle;
    
    private CustomCamera _camera;

    [SerializeField] private float _time;
    [SerializeField] private GameObject _obstacle;
    [Range(0, 10)] [SerializeField]
    private float _redaxMinTime;
    [Range(0, 100)] [SerializeField]
    private float _redaxMaxTime;
    private float _reduxTime;
    [SerializeField]
    private float MaxPositionY, MinPositionY;
    [SerializeField]
    private Vector2 _direction;
    private Transform _playerTransform;
    public RocketStrategy[] Rockets; 
    private void Start()
    {
        _camera = FindObjectOfType<CustomCamera>();
        _reduxTime = _time + Random.Range(_redaxMinTime, _redaxMaxTime);
        _playerTransform = FindObjectOfType<Player>().transform;
        StartCoroutine(Generate());

    }
    private void FixedUpdate()
    {
        // Move();
    }

    private Vector2 Rotate()
    {
        Vector2 center = _camera.transform.position;
        transform.position = new Vector2(center.x + Mathf.Cos(Angle) * Radius, center.y + Mathf.Sin(Angle) * Radius);
        Angle += Time.deltaTime * Speed;

        if (Angle >= 360)
        {
            Angle = 0;
        }
        return center;
    }
    private IEnumerator Generate()
    {
        int i = 0;
        while (true)
        {
            if (i > 0)
            {
                _reduxTime = _time + Random.Range(_redaxMinTime, _redaxMaxTime);
                Vector3 position = new Vector3(transform.position.x, _camera.transform.position.y + Random.Range(_camera.Height/2, -_camera.Height / 2), transform.position.z);
               // Instantiate(_obstacle, position, _obstacle.transform.rotation).SetDirection(_direction); ;

            }
            i++;
            yield return new WaitForSeconds(_reduxTime);
        }
    }
}

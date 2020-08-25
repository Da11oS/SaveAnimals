using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float _speed;
    private GenerateOfObstacles _generator;
    private GameObject _camera;
    private GameObject _warning;
    private void Start()
    {
        _generator = FindObjectOfType<GenerateOfObstacles>();
        _camera = FindObjectOfType<Camera>().gameObject;
        Vector3 generatePosition = new Vector3(_generator.WarningTransform.position.x, transform.position.y);
        _warning = Instantiate(_generator.WarningInstance, generatePosition, _generator.WarningInstance.transform.rotation, _camera.gameObject.transform);
    }

    private void Update()
    {
        Move();
        if (Vector3.Distance(transform.position, _camera.transform.position) > 40)
        {
            Destroy(gameObject);
        }
        if (_warning != null)
        {
            if (Mathf.Abs(transform.position.x - _generator.WarningTransform.position.x) < 0.5)
            {
                Destroy(_warning);
            }
        }
    }
    private void Move()
    {
        transform.Translate(transform.right * _speed * Time.deltaTime);
    }
 
}

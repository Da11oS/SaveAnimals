using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Strategy.Rocket
{
    interface IRocket
    {
        void Move();
    }
    [RequireComponent(typeof(Danger))]
    abstract public class RocketStrategy: MonoBehaviour, IRocket
    {
        public float Speed;
        public Vector2 StartPosition;
        public Vector2 Direction;

        [SerializeField]
        protected ObstaclesGenerator _generator;

        private float _maxDistance;
        private CustomCamera _camera;
        private Action Doing;
        private RocketWarning _warning;
        [SerializeField]
        private RocketWarning _warningInstance;
        public void Start()
        {
            StartPosition = transform.position;
            _camera = FindObjectOfType<CustomCamera>();         
        }
        public void Update()
        {
            var distance = GetDistanceToCamera();
            if (IsMove())
            {
                if(Doing == null)
                    Doing = Move;
                if (_warning == null)
                {
                    _warning = Instantiate(_warningInstance);
                    _warning.SetFather(gameObject);
                    _warning.SetPosition();
                }
            }
            Doing?.Invoke();
            if(distance > Mathf.Abs(_camera.Height + Speed * 6) && Doing!=null)
            {
                Destroy();
            }
            if (_warning != null && distance < new Vector2(_camera.Width, _camera.Height).magnitude)
            {
                Destroy(_warning.gameObject);
            }

        }
        abstract public void Move();
        public void Destroy()
        {
            Destroy(gameObject);
            if (_warning != null)
                Destroy(_warning.gameObject);
        }
        public bool IsMove()
        {
            return GetDistanceToCamera() < _camera.Height + Speed * 3;
        }
        public float GetDistanceToCamera()
        {
            return Vector2.Distance(transform.position, _camera.transform.position);
        }
        private bool IsVisible()
        {
            bool isHorizontal = transform.position.x > _camera.transform.position.x - _camera.Height / 2 &&
                transform.position.x  < _camera.transform.position.x + _camera.Height / 2;
            bool isVertical = transform.position.y > _camera.transform.position.y - _camera.Width / 2  && 
                transform.position.y < _camera.transform.position.y - _camera.Width / 2;
            return isHorizontal && isVertical;
        }
    }
}





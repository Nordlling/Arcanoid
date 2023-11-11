using System;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.GameGrid;
using UnityEngine;

namespace Main.Scripts.Logic.Platforms
{
    public class PlatformMovement : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _movingSpeed;
        [SerializeField] private float _minDistanceToMove;
        [SerializeField] private float _decelerationSpeed;
        [SerializeField] private Transform _ballPoint;

        private ZonesManager _zonesManager;
        private BallMovement _ball;
        private Camera _camera;


        private bool _started;
        private Vector2 _currentPosition;
        private Vector2 _targetPosition;
        private Vector2 _halfSize;

        private bool _move;
        private bool _decelerate;
        private float _currentSpeed;

        public void Construct(ZonesManager zonesManager, BallMovement ball, Camera viewCamera)
        {
            _zonesManager = zonesManager;
            InitBall(ball);
            _camera = viewCamera;

            _currentPosition = transform.position;
            _targetPosition.y = _currentPosition.y;
            _halfSize = _spriteRenderer.bounds.size / 2f;
        }

        public void InitBall(BallMovement ball)
        {
            _ball = ball;
            _ball.transform.position = _ballPoint.position;
            _started = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (!_started && _ball != null)
                {
                    _ball.transform.parent = null;
                    _ball.StartMove();
                    _started = true;
                    _ball = null;
                }
                _decelerate = true;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                _currentSpeed = _movingSpeed;
                _move = true;
                _decelerate = false;
            }
            
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                _targetPosition.x = mousePosition.x;
            }

            if (!_move)
            {
                return;
            }
            
            if (_decelerate)
            {
                _currentSpeed -= _decelerationSpeed;
                if (_currentSpeed <= 0f)
                {
                    _move = false;
                }
            }
            
            MovePlatform();
        }
        
        
        private void MovePlatform()
        {
            float deltaSpeed = _currentSpeed * Time.deltaTime;
            
            if (Math.Abs(_targetPosition.x - _currentPosition.x) < _minDistanceToMove)
            {
                _currentPosition = _targetPosition;
            } 
            else
            {
                Vector2 direction = (_targetPosition - _currentPosition).normalized;
                _currentPosition += direction * deltaSpeed;
            }
            
            _currentPosition.x = Mathf.Clamp(_currentPosition.x, _zonesManager.ScreenRect.xMin + _halfSize.x , _zonesManager.ScreenRect.xMax - _halfSize.x);
            transform.position = _currentPosition;
        }
        
    }
}
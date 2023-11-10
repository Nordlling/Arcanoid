using System;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.GameGrid;
using UnityEngine;

namespace Main.Scripts.Logic.Platforms
{
    public class PlatformMoving : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _movingSpeed;
        [SerializeField] private float _decelerationSpeed;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private ZonesManager _zonesManager;
        private BallMovement _ball;
        

        private Vector2 _currentPosition;
        private Vector2 _targetPosition;
        
        private bool _move;
        private bool _decelerate;
        private float _currentSpeed;
        private bool _started;

        public void Construct(ZonesManager zonesManager, BallMovement ball)
        {
            _zonesManager = zonesManager;
            _ball = ball;

            _currentPosition = transform.position;
            _targetPosition.y = _currentPosition.y;
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log(other.gameObject.name);
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (!_started)
                {
                    _ball.transform.parent = null;
                    _ball.StartMove();
                    _started = true;
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
            if (Math.Abs(_targetPosition.x - _currentPosition.x) < deltaSpeed)
            {
                return;
            }
            
            Vector2 direction = (_targetPosition - _currentPosition).normalized;
            
            _currentPosition += direction * deltaSpeed;
            if (!_zonesManager.IsInScreenZone(_currentPosition - (Vector2)_spriteRenderer.bounds.size / 2f) 
                || !_zonesManager.IsInScreenZone(_currentPosition + (Vector2)_spriteRenderer.bounds.size / 2f))
            {
                _currentPosition = transform.position;
                return;
            }
            
            transform.position = _currentPosition;
        }
        
    }
}
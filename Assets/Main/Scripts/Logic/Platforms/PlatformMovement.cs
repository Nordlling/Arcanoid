using System;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Logic.Zones;
using UnityEngine;

namespace Main.Scripts.Logic.Platforms
{
    public class PlatformMovement : MonoBehaviour, ILoseable, IWinable, IPrePlayable
    {
        public Transform BallPoint => _ballPoint;
        [SerializeField]  private Transform _ballPoint;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _movingSpeed;
        [SerializeField] private float _minDistanceToMove;
        [SerializeField] private float _decelerationSpeed;

        private ZonesManager _zonesManager;
        private Camera _camera;
        private ITimeProvider _timeProvider;

        private Vector2 _currentPosition;
        private Vector2 _targetPosition;
        private Vector2 _halfSize;

        private bool _stop;
        private bool _move;
        private bool _decelerate;
        private float _currentSpeed;

        public void Construct(ZonesManager zonesManager, Camera viewCamera, ITimeProvider timeProvider)
        {
            _zonesManager = zonesManager;
            _camera = viewCamera;
            _timeProvider = timeProvider;

            _currentPosition = transform.position;
            _targetPosition.y = _currentPosition.y;
            _halfSize = _spriteRenderer.bounds.size / 2f;
        }

        public void Lose()
        {
            _stop = true;
        }

        public void Win()
        {
            _stop = true;
        }

        public void PrePlay()
        {
            _stop = false;
        }

        private void Update()
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            
            if (_stop || _timeProvider.Stopped || !_zonesManager.IsInInputZone(mousePosition))
            {
                return;
            }
            
            if (Input.GetMouseButtonUp(0))
            {
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
                _targetPosition.x = mousePosition.x;
            }

            if (!_move)
            {
                return;
            }
            
            if (_decelerate)
            {
                DecelerateSpeed();
            }
            
            MovePlatform();
        }

        private void DecelerateSpeed()
        {
            _currentSpeed -= _decelerationSpeed;
            if (_currentSpeed <= 0f)
            {
                _move = false;
            }
        }

        private void MovePlatform()
        {
            float deltaSpeed = _currentSpeed * _timeProvider.DeltaTime;
            
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
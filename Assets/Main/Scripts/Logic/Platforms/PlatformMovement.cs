using System;
using System.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Logic.Platforms.PlatformSystems;
using Main.Scripts.Logic.Zones;
using UnityEngine;

namespace Main.Scripts.Logic.Platforms
{
    public class PlatformMovement : MonoBehaviour, ILoseable, IWinable, IPrePlayable, IRestartable
    {
        public Transform BallPoint => _ballPoint;
        [SerializeField]  private Transform _ballPoint;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private ISpeedPlatformSystem _speedPlatformSystem;
        private ZonesManager _zonesManager;
        private Camera _camera;
        private ITimeProvider _timeProvider;

        private Vector2 _currentPosition;
        private Vector2 _targetPosition;
        private Vector2 _halfSize;

        private bool _stop = true;
        private bool _move;
        private bool _decelerate;
        private float _currentSpeed;

        public void Construct(ISpeedPlatformSystem speedPlatformSystem, ZonesManager zonesManager, Camera viewCamera, ITimeProvider timeProvider)
        {
            _speedPlatformSystem = speedPlatformSystem;
            _zonesManager = zonesManager;
            _camera = viewCamera;
            _timeProvider = timeProvider;

            _currentPosition = transform.position;
            _targetPosition.y = _currentPosition.y;
        }

        public Task Lose()
        {
            _stop = true;
            return Task.CompletedTask;
        }

        public Task Win()
        {
            _stop = true;
            return Task.CompletedTask;
        }

        public Task PrePlay()
        {
            _stop = false;
            return Task.CompletedTask;
        }

        public Task Restart()
        {
            _stop = true;
            return Task.CompletedTask;
        }

        private void Update()
        {
            if (_stop)
            {
                return;
            }
            
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            
            if (_timeProvider.Stopped || !_zonesManager.IsInInputZone(mousePosition))
            {
                return;
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                _decelerate = true;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                _move = true;
                _decelerate = false;
            }
            
            if (Input.GetMouseButton(0))
            {
                _currentSpeed = _speedPlatformSystem.MovingSpeed;
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
            _currentSpeed -= _speedPlatformSystem.DecelerationSpeed;
            if (_currentSpeed <= 0f)
            {
                _move = false;
            }
        }

        private void MovePlatform()
        {
            float deltaSpeed = _currentSpeed * _timeProvider.DeltaTime;
            
            if (Math.Abs(_targetPosition.x - _currentPosition.x) < _speedPlatformSystem.MinDistanceToMove)
            {
                _currentPosition = _targetPosition;
            } 
            else
            {
                Vector2 direction = (_targetPosition - _currentPosition).normalized;
                _currentPosition += direction * deltaSpeed;
            }
            
            _halfSize = _spriteRenderer.bounds.size / 2f;
            _currentPosition.x = Mathf.Clamp(_currentPosition.x, _zonesManager.ScreenRect.xMin + _halfSize.x , _zonesManager.ScreenRect.xMax - _halfSize.x);
            transform.position = _currentPosition;
        }
    }
}
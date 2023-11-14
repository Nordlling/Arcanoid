using System;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.GameGrid;
using UnityEngine;

namespace Main.Scripts.Logic.Platforms
{
    public class PlatformMovement : MonoBehaviour, ILoseable, IWinable, IPrePlayable
    {
        public event Action OnStarted;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _movingSpeed;
        [SerializeField] private float _minDistanceToMove;
        [SerializeField] private float _decelerationSpeed;
        [SerializeField] private Transform _ballPoint;

        private ZonesManager _zonesManager;
        private BallMovement _ball;
        private Camera _camera;
        private ITimeProvider _timeProvider;

        private bool _started;
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

        public void InitBall(BallMovement ball)
        {
            _ball = ball;
            _ball.transform.position = _ballPoint.position;
            _started = false;
        }

        public void Lose()
        {
            _stop = true;
            _started = false;
        }

        public void Win()
        {
            _stop = true;
            _started = false;
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
                CheckBallToStartMove();
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
                _currentSpeed -= _decelerationSpeed;
                if (_currentSpeed <= 0f)
                {
                    _move = false;
                }
            }
            
            MovePlatform();
        }

        private void CheckBallToStartMove()
        {
            if (!_started && _ball != null)
            {
                OnStarted?.Invoke();
                _ball.transform.parent = null;
                _ball.StartMove();
                _started = true;
                _ball = null;
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
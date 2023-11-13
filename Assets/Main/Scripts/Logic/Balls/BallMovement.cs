using System;
using Main.Scripts.Infrastructure.Provides;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Balls
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        
        [SerializeField] private float _leftAngle;
        [SerializeField] private float _rightAngle;
        
        private ITimeProvider _timeProvider;
        private const float _epsilon = 0.01f;

        private Vector2 _direction;

        public void Construct(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
            _direction = Vector2.zero;
        }

        public void StartMove()
        {
            Vector2 startDirection = GenerateStartDirection();
            _rigidbody.AddForce(startDirection * _speed);
        }

        private void Update()
        {
            if (_rigidbody.velocity.magnitude != 0f)
            {
                _direction = _rigidbody.velocity.normalized;
            }

            float scaledSpeed = _speed * _timeProvider.TimeScale;
            _rigidbody.velocity = _direction * scaledSpeed;
        }


        private Vector2 GenerateStartDirection()
        {
            float angleDegrees = Random.Range(-_leftAngle, _rightAngle);
            Vector2 rotatedVector = Quaternion.Euler(0, 0, -angleDegrees) * Vector2.up;
            return rotatedVector.normalized;
        }
    }
}
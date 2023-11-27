using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Logic.Balls.BallSystems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Balls
{
    public class BallMovement : MonoBehaviour
    {
        private ITimeProvider _timeProvider;
        private IBallSpeedSystem _ballSpeedSystem;
        private Rigidbody2D _rigidbody;

        private Vector2 _direction;

        private const float _angleOffset = 0.1f;

        public bool Stop { get; set; }

        public void Construct(ITimeProvider timeProvider, IBallSpeedSystem ballSpeedSystem, Rigidbody2D ballRigidbody)
        {
            _timeProvider = timeProvider;
            _ballSpeedSystem = ballSpeedSystem;
            _rigidbody = ballRigidbody;

            _direction = Vector2.zero;
            _rigidbody.velocity = _direction;
        }
        
        public void StartMove(float leftAngle, float rightAngle)
        {
            Vector2 startDirection = GenerateStartDirection(leftAngle, rightAngle);
            _rigidbody.AddForce(startDirection * _ballSpeedSystem.CurrentSpeed);
        }

        public void StartMove()
        {
            Vector2 startDirection = GenerateStartDirection(_angleOffset, _angleOffset);
            _rigidbody.AddForce(startDirection * _ballSpeedSystem.CurrentSpeed);
        }

        private void Update()
        {
            if (_rigidbody.velocity.magnitude != 0f)
            {
                _direction = _rigidbody.velocity.normalized;
            }

            float scaledSpeed = _ballSpeedSystem.CurrentSpeed * _timeProvider.TimeScale;
            if (Stop)
            {
                scaledSpeed = 0f;
            }
            _rigidbody.velocity = _direction * scaledSpeed;
        }


        private Vector2 GenerateStartDirection(float leftAngle, float rightAngle)
        {
            float angleDegrees = Random.Range(-leftAngle, rightAngle);
            Vector2 rotatedVector = Quaternion.Euler(0, 0, -angleDegrees) * Vector2.up;
            return rotatedVector.normalized;
        }
    }
}
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Logic.Balls.BallSystems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Balls
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _leftAngle;
        [SerializeField] private float _rightAngle;

        private ITimeProvider _timeProvider;
        private IBallSpeedSystem _ballSpeedSystem;

        private Vector2 _direction;

        public bool Stop { get; set; }

        public void Construct(ITimeProvider timeProvider, IBallSpeedSystem ballSpeedSystem)
        {
            _timeProvider = timeProvider;
            _ballSpeedSystem = ballSpeedSystem;

            _direction = Vector2.zero;
        }
        
        public void StartMove(float leftAngle, float rightAngle)
        {
            Vector2 startDirection = GenerateStartDirection(leftAngle, rightAngle);
            _rigidbody.AddForce(startDirection * _ballSpeedSystem.CurrentSpeed);
        }

        public void StartMove()
        {
            Vector2 startDirection = GenerateStartDirection(_leftAngle, _rightAngle);
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
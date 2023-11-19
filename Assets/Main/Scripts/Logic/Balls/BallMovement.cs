using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.Difficulty;
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
        private IDifficultyService _difficultyService;

        private Vector2 _direction;

        public bool Stop { get; set; }

        public void Construct(ITimeProvider timeProvider, IDifficultyService difficultyService)
        {
            _timeProvider = timeProvider;
            _difficultyService = difficultyService;

            _direction = Vector2.zero;
        }

        public void StartMove()
        {
            Vector2 startDirection = GenerateStartDirection();
            _rigidbody.AddForce(startDirection * _difficultyService.Speed);
        }

        private void Update()
        {
            if (_rigidbody.velocity.magnitude != 0f)
            {
                _direction = _rigidbody.velocity.normalized;
            }

            float scaledSpeed = _difficultyService.Speed * _timeProvider.TimeScale;
            if (Stop)
            {
                scaledSpeed = 0f;
            }
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
using System;
using Main.Scripts.Logic.GameGrid;
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
        private const float _epsilon = 0.01f;

        public void StartMove()
        {
            Vector2 startDirection = GenerateStartDirection();
            _rigidbody.AddForce(startDirection * _speed);
        }

        private void Update()
        {
            CheckSpeed();
        }

        private void CheckSpeed()
        {
            if (Math.Abs(_rigidbody.velocity.magnitude - _speed) > _epsilon)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _speed;
            }
        }

        private Vector2 GenerateStartDirection()
        {
            float angleDegrees = Random.Range(-_leftAngle, _rightAngle);
            Vector2 rotatedVector = Quaternion.Euler(0, 0, -angleDegrees) * Vector2.up;
            return rotatedVector.normalized;
        }
    }
}
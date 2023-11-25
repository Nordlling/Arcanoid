using System;
using Main.Scripts.Infrastructure.Provides;
using UnityEngine;

namespace Main.Scripts.Logic.Boosts
{
    public class BoostMovement : MonoBehaviour
    {
        [SerializeField] private Vector2 _direction;
        [SerializeField] private float _speed;
        
        private ITimeProvider _timeProvider;

        private Vector2 _currentPosition;
        private Vector2 _currentVelocity;

        public void Construct(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
            _currentPosition = transform.position;
            _currentVelocity = _direction * _speed;
        }

        private void Update()
        {
            _currentPosition += _currentVelocity * _timeProvider.DeltaTime;
            transform.position = _currentPosition;
        }
    }
}
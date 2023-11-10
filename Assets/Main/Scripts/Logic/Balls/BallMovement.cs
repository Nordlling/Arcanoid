using Main.Scripts.Logic.GameGrid;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Balls
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private float _minAngle;
        
        [SerializeField] private float _leftAngle;
        [SerializeField] private float _rightAngle;
        private ZonesManager _zonesManager;


        public void Construct(ZonesManager zonesManager)
        {
            _zonesManager = zonesManager;
        }

        public void StartMove()
        {
            Vector2 startDirection = GenerateStartDirection();
            _rigidbody.AddForce(startDirection * _speed);
        }

        private void Update()
        {
            CheckSpeed();
            if (!_zonesManager.IsInLivingZone(transform.position))
            {
                Destroy(gameObject);
            }
        }
        
        private void CheckSpeed()
        {
            if (Math.Abs(_rigidbody.velocity.magnitude - _speed) > _epsilon)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _speed;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Vector2 velocity = _rigidbody.velocity;
            TryAngleControl(velocity, Vector2.right);
            TryAngleControl(velocity, Vector2.down);
            TryAngleControl(velocity, Vector2.left);
            TryAngleControl(velocity, Vector2.up);
        }

        private void TryAngleControl(Vector2 velocity, Vector2 normal)
        {
            float angle = Vector2.Angle(velocity, normal);
            
            if (angle >= _minAngle)
            {
                return;
            }

            float sign = Mathf.Sign(Vector3.Cross(velocity, normal).z);
            float rotationAngle = angle - _minAngle;
            velocity = Quaternion.AngleAxis(sign * rotationAngle, Vector3.forward) * velocity;
            _rigidbody.velocity = velocity;
        }

        private Vector2 GenerateStartDirection()
        {
            float angleDegrees = Random.Range(-_leftAngle, _rightAngle);
            Vector2 rotatedVector = Quaternion.Euler(0, 0, -angleDegrees) * Vector2.up;
            return rotatedVector.normalized;
        }
    }
}
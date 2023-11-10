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
            if (!_zonesManager.IsInLivingZone(transform.position))
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Vector2 velocity = _rigidbody.velocity;
            Vector2 normal = other.contacts[0].normal;
            float angle = Vector2.Angle(velocity, normal);
            float sign = Mathf.Sign(Vector3.Cross(velocity, normal).z);

            if (!(angle < _minAngle))
            {
                return;
            }
            
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
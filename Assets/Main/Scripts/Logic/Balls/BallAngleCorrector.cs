using Main.Scripts.Infrastructure.Services.Collision;
using UnityEngine;

namespace Main.Scripts.Logic.Balls
{
    public class BallAngleCorrector : MonoBehaviour, ICollisionInteractable
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [Range(0f, 45f)]
        [SerializeField] private float _minAngle;

        public void Interact()
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
    }
}
using Main.Scripts.Infrastructure.Services.Collision.CollisionHandlers;
using Main.Scripts.Infrastructure.Services.Collision.TriggerHandlers;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.CommonHandlers
{
    public class HealthHandler<T>  : ICollisionHandler, ITriggerHandler
    {
        private readonly int _damage;
        
        public HealthHandler(int damage)
        {
            _damage = damage;
        }
        
        public void Handle(GameObject acceptedObject, Collision2D enteredCollision)
        {
            if (acceptedObject.gameObject.TryGetComponent(out T _) && enteredCollision.gameObject.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }
        }

        public void Handle(GameObject acceptedObject, Collider2D enteredCollider)
        {
            if (acceptedObject.gameObject.TryGetComponent(out T _) && enteredCollider.gameObject.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }
        }
    }
}
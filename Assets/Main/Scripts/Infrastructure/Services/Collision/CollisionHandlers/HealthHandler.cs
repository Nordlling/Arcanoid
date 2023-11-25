using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.CollisionHandlers
{
    public class HealthHandler : ICollisionHandler
    {
        public void Handle(GameObject acceptedObject, Collision2D enteredCollision)
        {
            if (enteredCollision.gameObject.TryGetComponent(out Health health))
            {
                health.TakeDamage(1);
            }
        }
        
    }
}
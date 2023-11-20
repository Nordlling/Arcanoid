using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.CollisionHandlers
{
    public class HealthHandler : ICollisionHandler
    {
        public void Handle(GameObject acceptedObject, GameObject enteredObject)
        {
            if (enteredObject.TryGetComponent(out Health health))
            {
                health.TakeDamage(1);
            }
        }
        
    }
}
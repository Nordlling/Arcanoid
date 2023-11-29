using Main.Scripts.Logic.Platforms;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.CollisionHandlers
{
    public class ShakeOnHitHandler : ICollisionHandler
    {
        public void Handle(GameObject acceptedObject, Collision2D enteredCollision)
        {
            if (enteredCollision.gameObject.TryGetComponent(out Shaker shakeOnHit))
            {
                shakeOnHit.Shake(enteredCollision.relativeVelocity.normalized * -1f);
            }
        }
        
    }
}
using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.CollisionHandlers
{
    public class ChangeAngleOnHitHandler : ICollisionHandler
    {
        public void Handle(GameObject acceptedObject, Collision2D enteredCollision)
        {
            if (enteredCollision.gameObject.TryGetComponent(out ChangeAngleOnHit _) && acceptedObject.TryGetComponent(out BallAngleCorrector ballAngleCorrector))
            {
                ballAngleCorrector.Interact();
            }
        }
        
    }
}
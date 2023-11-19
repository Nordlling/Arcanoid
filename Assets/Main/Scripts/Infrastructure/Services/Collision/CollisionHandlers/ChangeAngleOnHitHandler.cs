using Main.Scripts.Logic.Balls;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.CollisionHandlers
{
    public class ChangeAngleOnHitHandler : ICollisionHandler
    {
        public void Handle(GameObject acceptedObject, GameObject enteredObject)
        {
            if (enteredObject.TryGetComponent(out ChangeAngleOnHit _) && acceptedObject.TryGetComponent(out BallAngleCorrector ballAngleCorrector))
            {
                ballAngleCorrector.Interact();
            }
        }
        
    }
}
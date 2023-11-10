using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision
{
    public class BallCollisionService : IBallCollisionService
    {
        public void CollisionProcessing(CollisionDetector collisionDetector, GameObject enteredObject)
        {
            if (enteredObject.TryGetComponent(out ChangeAngleOnHit _) && collisionDetector.TryGetComponent(out BallAngleCorrector ballAngleCorrector))
            {
                ballAngleCorrector.CorrectAngle();
            }

            if (enteredObject.TryGetComponent(out Health health))
            {
                health.Hit();
            }
            
            if (enteredObject.TryGetComponent(out BreaksVisual breaksVisual))
            {
                breaksVisual.AddBreak();
            }

            if (enteredObject.TryGetComponent(out Explosion explosion))
            {
                explosion.Explode();
            }
            
        }
    }
}
using Main.Scripts.Infrastructure.Services.Collision.CollisionHandlers;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision
{
    public class BallCollisionService : IBallCollisionService
    {
        
        private readonly ICollisionHandler[] _handlers = {
            new ChangeAngleOnHitHandler(),
            new SimpleHandler<Health>(),
            new SimpleHandler<BreaksVisual>(),
            new SimpleHandler<Explosion>(),
        };

        public void CollisionProcessing(CollisionDetector collisionDetector, GameObject enteredObject)
        {
            foreach (var handler in _handlers)
            {
                handler.Handle(collisionDetector.gameObject, enteredObject);
            }
        }
    }
}
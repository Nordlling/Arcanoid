using Main.Scripts.Infrastructure.Services.Collision.CollisionHandlers;
using Main.Scripts.Infrastructure.Services.Collision.TriggerHandlers;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Boosts;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision
{
    public class BallCollisionService : IBallCollisionService
    {
        
        private readonly ICollisionHandler[] _collisionHandlers = {
            new ChangeAngleOnHitHandler(),
            new HealthHandler(),
            new EnteredCollisionHandler<Explosion>(),
            new EnteredCollisionHandler<BoostKeeper>()
        };
        
        private readonly ITriggerHandler[] _triggerHandlers = {
        };

        private readonly InteractionsProcessor _interactionsProcessor = new();

        public void CollisionProcessing(CollisionDetector collisionDetector, Collision2D enteredCollision)
        {
            _interactionsProcessor.CollisionProcessing(_collisionHandlers, collisionDetector, enteredCollision);
        }

        public void TriggerProcessing(CollisionDetector collisionDetector, Collider2D enteredCollision)
        {
            _interactionsProcessor.TriggerProcessing(_triggerHandlers, collisionDetector, enteredCollision);
        }
    }
}
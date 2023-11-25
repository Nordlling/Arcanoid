using Main.Scripts.Infrastructure.Services.Collision.CollisionHandlers;
using Main.Scripts.Infrastructure.Services.Collision.TriggerHandlers;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision
{
    public class BoostCollisionService : IBoostCollisionService
    {
        
        private readonly ICollisionHandler[] _collisionHandlers = {
        };
        
        private readonly ITriggerHandler[] _triggerHandlers = {
            new ActivateBoostOnHitHandler(),
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
using Main.Scripts.Infrastructure.Services.Collision.CollisionHandlers;
using Main.Scripts.Infrastructure.Services.Collision.TriggerHandlers;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision
{
    public class InteractionsProcessor
    {
        public void CollisionProcessing(ICollisionHandler[] collisionHandlers, CollisionDetector collisionDetector, Collision2D enteredCollision)
        {
            foreach (var handler in collisionHandlers)
            {
                handler.Handle(collisionDetector.gameObject, enteredCollision);
            }
        }

        public void TriggerProcessing(ITriggerHandler[] triggerHandlers, CollisionDetector collisionDetector, Collider2D enteredCollision)
        {
            foreach (var handler in triggerHandlers)
            {
                handler.Handle(collisionDetector.gameObject, enteredCollision);
            }
        }
    }
}
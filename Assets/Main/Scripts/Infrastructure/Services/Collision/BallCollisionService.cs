using Main.Scripts.Infrastructure.Services.Collision.CollisionHandlers;
using Main.Scripts.Infrastructure.Services.Collision.CommonHandlers;
using Main.Scripts.Infrastructure.Services.Collision.TriggerHandlers;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Boosts;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision
{
    public class BallCollisionService : IBallCollisionService
    {
        private readonly ICollisionHandler[] _collisionHandlers = {
            new ChangeAngleOnHitHandler(),
            new ShakeOnHitHandler(),
            new HealthHandler<Component>(1),
            new EnteredCollisionHandler<Explosion>(),
            new EnteredCollisionHandler<ExtraBall>(),
            new EnteredCollisionHandler<BoostKeeper>(),
            new AcceptedCollisionHandler<HitEffect>()
        };
        
        private readonly ITriggerHandler[] _triggerHandlers = {
            new HealthHandler<Fireball>(999),
            new EnteredTriggerHandler<Component, Explosion>(),
            new EnteredTriggerHandler<Component, ExtraBall>(),
            new EnteredTriggerHandler<Component, BoostKeeper>(),
            new EnteredTriggerHandler<Fireball, DestroyOnFireball>(),
            new HitOnBulletHandler(),
            new AcceptedTriggerHandler<Bullet, DestroyBulletOnHit>()
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
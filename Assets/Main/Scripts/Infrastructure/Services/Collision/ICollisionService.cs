using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision
{
    public interface ICollisionService : IService
    {
        void CollisionProcessing(CollisionDetector collisionDetector, Collision2D enteredCollision);
        void TriggerProcessing(CollisionDetector collisionDetector, Collider2D enteredCollision);
    }
    
    public interface IBallCollisionService : ICollisionService
    {
    }
    
    public interface IBoostCollisionService : ICollisionService
    {
    }
}
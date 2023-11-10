using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision
{
    public interface ICollisionService : IService
    {
        void CollisionProcessing(CollisionDetector collisionDetector, GameObject enteredObject);
    }
}
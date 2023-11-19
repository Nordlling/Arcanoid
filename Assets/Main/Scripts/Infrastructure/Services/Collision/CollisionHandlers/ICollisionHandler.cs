using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.CollisionHandlers
{
    public interface ICollisionHandler
    {
        void Handle(GameObject acceptedObject, GameObject enteredObject);
    }
}
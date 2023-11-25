using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.CollisionHandlers
{
    public class AcceptedCollisionHandler<T> : ICollisionHandler where T : ICollisionInteractable
    {
        public void Handle(GameObject acceptedObject, Collision2D enteredCollision)
        {
            if (acceptedObject.TryGetComponent(out T collisionInteractable))
            {
                collisionInteractable.Interact();
            }
        }
        
    }
}
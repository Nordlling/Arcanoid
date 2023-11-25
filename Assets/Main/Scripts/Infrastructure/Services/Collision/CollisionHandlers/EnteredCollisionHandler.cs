using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.CollisionHandlers
{
    public class EnteredCollisionHandler<T> : ICollisionHandler where T : ICollisionInteractable
    {
        public void Handle(GameObject acceptedObject, Collision2D enteredCollision)
        {
            if (enteredCollision.gameObject.TryGetComponent(out T collisionInteractable))
            {
                collisionInteractable.Interact();
            }
        }
        
    }
}
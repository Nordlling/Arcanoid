using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.CollisionHandlers
{
    public class SimpleHandler<T> : ICollisionHandler where T : ICollisionInteractable
    {
        public void Handle(GameObject acceptedObject, GameObject enteredObject)
        {
            if (enteredObject.TryGetComponent(out T collisionInteractable))
            {
                collisionInteractable.Interact();
            }
        }
        
    }
}
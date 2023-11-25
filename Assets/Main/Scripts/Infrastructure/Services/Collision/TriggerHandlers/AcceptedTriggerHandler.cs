using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.TriggerHandlers
{
    public class AcceptedTriggerHandler<T> : ITriggerHandler where T : ITriggerInteractable
    {
        public void Handle(GameObject acceptedObject, Collider2D enteredCollision)
        {
            if (acceptedObject.TryGetComponent(out T collisionInteractable))
            {
                collisionInteractable.Interact();
            }
        }
        
    }
}
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.TriggerHandlers
{
    public class EnteredTriggerHandler<T> : ITriggerHandler where T : ITriggerInteractable
    {
        public void Handle(GameObject acceptedObject, Collider2D enteredCollision)
        {
            if (enteredCollision.gameObject.TryGetComponent(out T collisionInteractable))
            {
                collisionInteractable.Interact();
            }
        }
        
    }
}
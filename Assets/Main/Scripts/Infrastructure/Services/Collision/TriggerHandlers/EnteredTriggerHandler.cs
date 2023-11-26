using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.TriggerHandlers
{
    public class EnteredTriggerHandler<T1, T2> : ITriggerHandler where T2 : ITriggerInteractable
    {
        public void Handle(GameObject acceptedObject, Collider2D enteredCollision)
        {
            if (acceptedObject.TryGetComponent(out T1 _) && enteredCollision.gameObject.TryGetComponent(out T2 collisionInteractable))
            {
                collisionInteractable.Interact();
            }
        }
        
    }
}
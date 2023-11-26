using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.TriggerHandlers
{
    public class AcceptedTriggerHandler<T1, T2> : ITriggerHandler where T1 : ITriggerInteractable
    {
        public void Handle(GameObject acceptedObject, Collider2D enteredCollision)
        {
            if (acceptedObject.gameObject.TryGetComponent(out T1 collisionInteractable) && enteredCollision.gameObject.TryGetComponent(out T2 _))
            {
                collisionInteractable.Interact();
            }
        }
        
    }
}
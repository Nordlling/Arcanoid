using Main.Scripts.Logic.Platforms;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.TriggerHandlers
{
    public class ActivateBoostOnHitHandler : ITriggerHandler
    {
        public void Handle(GameObject acceptedObject, Collider2D enteredCollider)
        {
            if (enteredCollider.gameObject.TryGetComponent(out ActivateBoostOnHit _) && acceptedObject.TryGetComponent(out ITriggerInteractable triggerInteractable))
            {
                triggerInteractable.Interact();
            }
        }
        
    }
}
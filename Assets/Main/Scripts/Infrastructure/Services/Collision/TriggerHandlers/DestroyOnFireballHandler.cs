using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.TriggerHandlers
{
    public class DestroyOnFireballHandler : ITriggerHandler
    {
        public void Handle(GameObject acceptedObject, Collider2D enteredCollision)
        {
            if (acceptedObject.TryGetComponent(out Fireball _) && enteredCollision.gameObject.TryGetComponent(out DestroyOnFireball destroyOnFireball))
            {
                destroyOnFireball.Interact();
            }
        }
        
    }
}
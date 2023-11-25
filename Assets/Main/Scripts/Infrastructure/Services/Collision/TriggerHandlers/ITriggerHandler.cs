using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.TriggerHandlers
{
    public interface ITriggerHandler
    {
        void Handle(GameObject acceptedObject, Collider2D enteredCollision);
    }
}
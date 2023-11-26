using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision.TriggerHandlers
{
    public class HitOnBulletHandler : ITriggerHandler
    {
        public void Handle(GameObject acceptedObject, Collider2D enteredCollision)
        {
            if (acceptedObject.TryGetComponent(out Bullet bullet) && enteredCollision.gameObject.TryGetComponent(out Health health))
            {
                health.TakeDamage(bullet.MachineGunConfig.Damage);
            }
        }
        
    }
}
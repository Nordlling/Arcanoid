using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.Balls.BallContainers;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.BallComponents
{
    public class FireballComponentFactory : IBallComponentFactory
    {
        public Sprite FireballSprite;
        public string FireballEffectKey;
        
        public void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono
        {
            if (unit is not Ball ball)
            {
                return;
            }
            Fireball fireball = ball.AddComponent<Fireball>();
            fireball.Construct(
                serviceContainer.Get<IEffectFactory>(), 
                serviceContainer.Get<IBallContainer>(), 
                ball.SpriteRenderer,
                FireballSprite,
                FireballEffectKey);
        }
        
        public void RemoveComponent<T>(T unit) where T : SpawnableItemMono
        {
            if (unit.TryGetComponent(out Fireball fireball))
            {
                fireball.Despawn();
                Object.Destroy(fireball);
            }
        }
    }
}
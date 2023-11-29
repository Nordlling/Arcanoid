using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Pool;
using UnityEngine;

namespace Main.Scripts.Logic.Boosts
{
    public class Boost : SpawnableItemMono
    {
        public string ID { get; private set; }
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public CollisionDetector CollisionDetector => _collisionDetector;
        public BoostMovement BoostMovement => _boostMovement;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CollisionDetector _collisionDetector;
        [SerializeField] private BoostMovement _boostMovement;
        
        private IBoostContainer _boostContainer;
        private string _destroyEffectKey;
        private IEffectFactory _effectFactory;

        public void Construct(string id, IBoostContainer boostContainer, IEffectFactory effectFactory, string destroyEffectKey)
        {
            ID = id;
            _boostContainer = boostContainer;
            _effectFactory = effectFactory;
            _destroyEffectKey = destroyEffectKey;
        }
        
        public void Destroy()
        {
            _effectFactory.SpawnAndEnable(transform.position, transform.localScale, _destroyEffectKey);
            _boostContainer.RemoveBoost(this);
        }
    }
}
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

        public void Construct(string id, IBoostContainer boostContainer)
        {
            ID = id;
            _boostContainer = boostContainer;
        }
        
        public void Destroy()
        {
            _boostContainer.RemoveBoost(this);
        }
    }
}
using Main.Scripts.Factory;
using Main.Scripts.Pool;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Block : SpawnableItemMono
    {
        public string ID;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public BoxCollider2D Collider => _collider;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider2D _collider;
       
        private IBlockFactory _blockFactory;

        public void Construct(IBlockFactory blockFactory, string id)
        {
            ID = id;
            _blockFactory = blockFactory;
        }

        public void Subscribe(IDieable dieable)
        {
            dieable.OnDied += Die;
        }

        private void Die()
        {
            _blockFactory.Despawn(this);
        }
    }
}
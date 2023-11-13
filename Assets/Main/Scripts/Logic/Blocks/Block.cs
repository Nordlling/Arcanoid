using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.Services.LevelMap;
using Main.Scripts.Pool;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Block : SpawnableItemMono
    {
        public string ID;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public SpriteRenderer BreakSpriteRenderer => _breakSpriteRenderer;
        public BoxCollider2D Collider => _collider;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private SpriteRenderer _breakSpriteRenderer;
        [SerializeField] private BoxCollider2D _collider;
       
        private IBlockFactory _blockFactory;
        private IGameGridService _gameGridService;

        public void Construct(IBlockFactory blockFactory, IGameGridService gameGridService, string id)
        {
            _blockFactory = blockFactory;
            _gameGridService = gameGridService;
            ID = id;
        }

        public void Subscribe(IDieable dieable)
        {
            dieable.OnDied += Die;
        }

        private void Die()
        {
            _gameGridService.RemoveBlockFromGrid(this);
            _blockFactory.Despawn(this);
        }
    }
}
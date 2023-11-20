using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.Services.GameGrid;
using Main.Scripts.Pool;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Block : SpawnableItemMono
    {
        public string ID { get; private set; }
        public Vector2Int GridPosition { get; set; }
        
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
            _breakSpriteRenderer.sprite = null;
            ID = id;
        }

        public void Destroy()
        {
            _gameGridService.RemoveAt(this);
        }
    }
}
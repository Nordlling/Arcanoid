using Main.Scripts.Infrastructure.Services.GameGrid;
using Main.Scripts.Pool;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Block : SpawnableItemMono
    {
        public string ID { get; private set; }
        public bool CheckToWin  { get; private set; }
        public Vector2Int GridPosition { get; set; }
        public Vector3 SizeRatio { get; set; }
        
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public SpriteRenderer BreakSpriteRenderer => _breakSpriteRenderer;
        public BoxCollider2D Collider => _collider;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private SpriteRenderer _breakSpriteRenderer;
        [SerializeField] private BoxCollider2D _collider;
       
        private IGameGridService _gameGridService;

        public void Construct(IGameGridService gameGridService, string id, bool blockInfoCheckToWin)
        {
            _gameGridService = gameGridService;
            ID = id;
            CheckToWin = blockInfoCheckToWin;
            _breakSpriteRenderer.sprite = null;
        }

        public void Destroy()
        {
            _gameGridService.TryRemoveAt(this, out _);
        }
    }
}
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Pool;
using UnityEngine;

namespace Main.Scripts.Logic.Balls
{
    public class Ball : SpawnableItemMono
    {
        public string ID;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public CircleCollider2D Collider => _collider;
        public BoundsChecker BoundsChecker => _boundsChecker;
        public CollisionDetector CollisionDetector => _collisionDetector;
        public BallMovement BallMovement => _ballMovement;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private BoundsChecker _boundsChecker;
        [SerializeField] private CollisionDetector _collisionDetector;
        [SerializeField] private BallMovement _ballMovement;
       
        private IBallFactory _ballFactory;
        private IBallManager _ballManager;

        public void Construct(IBallFactory ballFactory, IBallManager ballManager, string id)
        {
            _ballFactory = ballFactory;
            _ballManager = ballManager;
            ID = id;
        }

        public void Subscribe(IDieable dieable)
        {
            dieable.OnDied += Die;
        }

        private void Die()
        {
            _ballManager.RemoveBall(this);
        }
    }
}
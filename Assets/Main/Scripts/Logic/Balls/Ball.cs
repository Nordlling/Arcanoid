using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Balls.BallContainers;
using Main.Scripts.Pool;
using UnityEngine;

namespace Main.Scripts.Logic.Balls
{
    public class Ball : SpawnableItemMono
    {
        public string ID;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public CircleCollider2D Collider => _collider;
        public CollisionDetector CollisionDetector => _collisionDetector;
        public Rigidbody2D Rigidbody => _rigidbody;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private CollisionDetector _collisionDetector;
        [SerializeField] private Rigidbody2D _rigidbody;
        
        private IBallContainer _ballContainer;

        public void Construct(string id, IBallContainer ballContainer)
        {
            ID = id;
            _ballContainer = ballContainer;
        }

        public void Destroy()
        {
            _ballContainer.RemoveBall(this);
        }
    }
}
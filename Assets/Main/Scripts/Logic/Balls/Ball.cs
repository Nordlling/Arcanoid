using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.Services.Collision;
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
       
        private IBallFactory _ballFactory;

        public void Construct(string id, IBallFactory ballFactory)
        {
            ID = id;
            _ballFactory = ballFactory;
        }
    }
}
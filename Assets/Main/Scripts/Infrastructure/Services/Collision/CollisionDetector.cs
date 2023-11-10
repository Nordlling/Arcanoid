using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Collision
{
    public class CollisionDetector : MonoBehaviour
    {
        private ICollisionService _collisionService;

        public void Construct(ICollisionService collisionService)
        {
            _collisionService = collisionService;
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            _collisionService.CollisionProcessing(this, other.gameObject);
        }
        
    }
}
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Explosion : MonoBehaviour
    {
        private LayerMask _ballLayer;
        
        public void Construct(LayerMask ballLayer)
        {
            _ballLayer = ballLayer;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // if (_ballLayer == (_ballLayer | (1 << collision.gameObject.layer)))
            // {
                transform.localScale *= 0.5f;
            // }
        }
    }
}
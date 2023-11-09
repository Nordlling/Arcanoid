using System;
using Main.Scripts.Factory;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Health : MonoBehaviour
    {
        private int _healthCount;
        private int _currentHealthCount;
        private LayerMask _ballLayer;
        private IBlockFactory _blockFactory;

        public event Action OnDied;

        public void Construct(int healthCount, LayerMask ballLayer)
        {
            _healthCount = healthCount;
            _ballLayer = ballLayer;
            
            _currentHealthCount = _healthCount;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // if (_ballLayer == (_ballLayer | (1 << collision.gameObject.layer)))
            // {
                _currentHealthCount--;
                if (_currentHealthCount == 0)
                {
                    OnDied?.Invoke();
                }
            // }
        }
    }
}
using System;
using Main.Scripts.Factory;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Health : MonoBehaviour, IDieable
    {
        private int _healthCount;
        private int _currentHealthCount;

        public event Action OnDied;

        public void Construct(int healthCount)
        {
            _healthCount = healthCount;
            
            _currentHealthCount = _healthCount;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
                _currentHealthCount--;
                if (_currentHealthCount == 0)
                {
                    OnDied?.Invoke();
                }
        }
    }

    public interface IDieable
    {
        event Action OnDied;
    }
}
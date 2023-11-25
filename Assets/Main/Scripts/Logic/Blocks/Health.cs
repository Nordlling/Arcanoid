using Main.Scripts.Infrastructure.Services.Collision;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Health : MonoBehaviour, ICollisionInteractable
    {
        private int _healthCount;
        private int _currentHealthCount;
        
        public void Construct(int healthCount)
        {
            _healthCount = healthCount;

            _currentHealthCount = _healthCount;
        }

        public void Interact()
        {
            TakeDamage(1);
        }
        
        public void TakeDamage(int count)
        {
            _currentHealthCount -= count;
            if (TryGetComponent(out HealthVisual healthVisual))
            {
                healthVisual.RefreshDamageView(count);
            }
            
            if (_currentHealthCount <= 0 && TryGetComponent(out Block block))
            {
                if (healthVisual is not null)
                {
                    healthVisual.RefreshDieView();
                }
                block.Destroy();
            }
        }
    }
}
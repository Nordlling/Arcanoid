using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Boosts;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Health : MonoBehaviour, ICollisionInteractable
    {
        private int _healthCount;
        private int _currentHealthCount;
        private Block _block;

        public void Construct(int healthCount, Block block)
        {
            _healthCount = healthCount;
            _block = block;

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

            if (_currentHealthCount > 0)
            {
                return;
            }
            
            if (TryGetComponent(out BoostKeeper boostKeeper))
            {
                boostKeeper.Interact();
                return;
            }
            
            if (healthVisual is not null)
            {
                healthVisual.RefreshDieView();
            }
                
            _block.Destroy();
        }
        
    }
}
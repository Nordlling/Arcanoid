using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Infrastructure.Services.GameGrid;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class DestroyOnFireball : MonoBehaviour, ITriggerInteractable
    {
        private IGameGridService _gameGridService;

        public void Interact()
        {
            if (TryGetComponent(out Block block))
            {
                block.Destroy();
            }
        }
        
    }
}
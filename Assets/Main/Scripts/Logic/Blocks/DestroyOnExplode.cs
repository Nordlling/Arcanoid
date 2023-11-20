using Main.Scripts.Infrastructure.Services.GameGrid;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class DestroyOnExplode : MonoBehaviour
    {
        private IGameGridService _gameGridService;

        public void Explode()
        {
            if (TryGetComponent(out Block block))
            {
                block.Destroy();
            }
        }
    }
}
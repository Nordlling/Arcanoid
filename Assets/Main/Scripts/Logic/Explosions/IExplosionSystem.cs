using Main.Scripts.Configs.Boosts;
using UnityEngine;

namespace Main.Scripts.Logic.Explosions
{
    public interface IExplosionSystem
    {
        void ExplodeBlocks(Vector2Int gridPosition, ExplosionConfig explosionConfig);
    }
}
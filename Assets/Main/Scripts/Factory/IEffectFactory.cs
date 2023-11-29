using Main.Scripts.Logic.Effects;
using UnityEngine;

namespace Main.Scripts.Factory
{
    public interface IEffectFactory
    {
        Effect Spawn(SpawnContext spawnContext);
        Effect SpawnAndEnable(Vector2 position, string effectKey);
        Effect SpawnAndEnable(Vector2 position, Vector3 scale, string effectKey);
        void Despawn(Effect effect);
    }
}
using Main.Scripts.Logic.Effects;
using UnityEngine;

namespace Main.Scripts.Factory
{
    public interface IEffectFactory
    {
        Effect Spawn(SpawnContext spawnContext);
        public Effect SpawnAndEnable(Vector2 position, string effectKey);
        void Despawn(Effect effect);
    }
}
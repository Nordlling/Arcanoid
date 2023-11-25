using Main.Scripts.Logic.Effects;

namespace Main.Scripts.Factory
{
    public interface IEffectFactory
    {
        Effect Spawn(SpawnContext spawnContext);
        void Despawn(Effect effect);
    }
}
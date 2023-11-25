using Main.Scripts.Logic.Boosts;

namespace Main.Scripts.Factory
{
    public interface IBoostFactory
    {
        Boost Spawn(SpawnContext spawnContext);
        void Despawn(Boost effect);
    }
}
using Main.Scripts.Logic.Balls;

namespace Main.Scripts.Factory
{
    public interface IBallFactory
    {
        Ball Spawn(SpawnContext spawnContext);
        void Despawn(Ball gameObject);
    }
}
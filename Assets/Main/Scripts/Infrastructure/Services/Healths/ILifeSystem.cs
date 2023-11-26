using Main.Scripts.Configs.Boosts;

namespace Main.Scripts.Infrastructure.Services.Healths
{
    public interface ILifeSystem
    {
        void ActivateLifeBoost(LifeConfig lifeConfig);
    }
}
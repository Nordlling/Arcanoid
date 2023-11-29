using Main.Scripts.Configs.Boosts;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Healths
{
    public interface ILifeSystem
    {
        void ActivateLifeBoost(LifeConfig lifeConfig, Vector2 position, Vector3 scale);
    }
}
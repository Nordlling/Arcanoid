using Main.Scripts.Configs.Boosts;
using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Balls.BallBoosts;
using Main.Scripts.Logic.Balls.BallSystems;
using Main.Scripts.Logic.Boosts;
using Main.Scripts.Logic.Platforms.PlatformBoosts;
using Main.Scripts.Logic.Platforms.PlatformSystems;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.BoostComponents
{
    public class MachineGunComponentFactory : IBoostComponentFactory
    {   
        public MachineGunConfig MachineGunConfig;
        
        public void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono
        {
            if (unit is not Boost boost)
            {
                return;
            }
            MachineGunBoost machineGunBoost = boost.AddComponent<MachineGunBoost>();
            machineGunBoost.Construct(boost, MachineGunConfig, serviceContainer.Get<IMachineGunSystem>());
        }
        
        public void RemoveComponent<T>(T unit) where T : SpawnableItemMono
        {
            if (unit.TryGetComponent(out MachineGunBoost machineGunBoost))
            {
                Object.Destroy(machineGunBoost);
            }
        }
        
    }
}
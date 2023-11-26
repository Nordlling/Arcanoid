using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Boosts;
using Main.Scripts.Logic.Platforms.PlatformSystems;
using UnityEngine;

namespace Main.Scripts.Logic.Platforms.PlatformBoosts
{
    public class MachineGunBoost : MonoBehaviour, ITriggerInteractable
    {
        private Boost _boost;
        private MachineGunConfig _machineGunConfig;
        private IMachineGunSystem _machineGunSystem;

        public void Construct(Boost boost, MachineGunConfig machineGunConfig, IMachineGunSystem machineGunSystem)
        {
            _boost = boost;
            _machineGunConfig = machineGunConfig;
            _machineGunSystem = machineGunSystem;
        }

        public void Interact()
        {
            _machineGunSystem.ActivateMachineGunBoost(_machineGunConfig);
            _boost.Destroy();
        }
    }
}
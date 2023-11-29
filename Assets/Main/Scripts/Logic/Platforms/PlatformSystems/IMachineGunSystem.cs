using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Services.BoostTimers;

namespace Main.Scripts.Logic.Platforms.PlatformSystems
{
    public interface IMachineGunSystem : ITimerBoost
    {
        void ActivateMachineGunBoost(MachineGunConfig machineGunConfig, string boostId);
    }
}
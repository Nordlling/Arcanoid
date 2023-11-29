using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Services.BoostTimers;

namespace Main.Scripts.Logic.Platforms.PlatformSystems
{
    public interface ISpeedPlatformSystem : ITimerBoost
    {
        void ActivateSpeedBoost(SpeedPlatformConfig speedPlatformConfig, string boostId);
        float MovingSpeed { get; }
        float DecelerationSpeed { get; }
        float MinDistanceToMove { get; }
    }
}
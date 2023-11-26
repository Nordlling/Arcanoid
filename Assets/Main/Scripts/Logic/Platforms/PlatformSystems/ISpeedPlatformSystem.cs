using Main.Scripts.Configs.Boosts;

namespace Main.Scripts.Logic.Platforms.PlatformSystems
{
    public interface ISpeedPlatformSystem
    {
        void ActivateSpeedBoost(SpeedPlatformConfig speedPlatformConfig);
        float MovingSpeed { get; }
        float DecelerationSpeed { get; }
        float MinDistanceToMove { get; }
    }
}
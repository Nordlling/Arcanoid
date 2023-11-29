using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Services.BoostTimers;

namespace Main.Scripts.Logic.Platforms.PlatformSystems
{
    public interface ISizePlatformSystem : ITimerBoost
    {
        void ActivateSizeBoost(SizePlatformConfig sizePlatformConfig, string boostId);
    }
}
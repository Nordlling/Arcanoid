using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Services.BoostTimers;

namespace Main.Scripts.Logic.Balls.BallSystems
{
    public interface IFireballSystem : ITimerBoost
    {
        void ActivateFireballBoost(FireballConfig fireballConfig, string boostId);
    }
}
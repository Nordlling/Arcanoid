using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Services.BoostTimers;

namespace Main.Scripts.Logic.Balls.BallSystems
{
    public interface IBallSpeedSystem : ITimerBoost
    {
        float CurrentSpeed { get; }
        void ActivateSpeedBoost(SpeedBallConfig speedBallConfig, string boostId);
    }
}
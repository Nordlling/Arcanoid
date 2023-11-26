using Main.Scripts.Configs.Boosts;

namespace Main.Scripts.Logic.Balls.BallSystems
{
    public interface IBallSpeedSystem
    {
        float CurrentSpeed { get; }
        void ActivateSpeedBoost(SpeedBallConfig speedBallConfig);
    }
}
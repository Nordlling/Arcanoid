using Main.Scripts.Configs.Boosts;

namespace Main.Scripts.Logic.Balls
{
    public interface IBallSpeedSystem
    {
        float CurrentSpeed { get; }
        void ActivateSpeedBoost(SpeedBallConfig speedBallConfig);
    }
}
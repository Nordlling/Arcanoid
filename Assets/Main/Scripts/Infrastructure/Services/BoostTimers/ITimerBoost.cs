namespace Main.Scripts.Infrastructure.Services.BoostTimers
{
    public interface ITimerBoost
    {
        string BoostId { get; }
        float BoostTime { get; }
        bool IsActive { get; }
    }
}
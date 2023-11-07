namespace Main.Scripts.Infrastructure.Provides
{
    public interface ITimeProvider
    {
        bool Stopped { get; }
        
        float GetDeltaTime();
        float GetTimeScale();
        
        void StopTime();
        void SlowTime(float timeScale);
        void TurnBackTime();
        void SetRealTime();
    }
}
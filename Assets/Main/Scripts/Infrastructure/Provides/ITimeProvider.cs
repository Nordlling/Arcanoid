namespace Main.Scripts.Infrastructure.Provides
{
    public interface ITimeProvider
    {
        bool Stopped { get; }
        
        float DeltaTime { get; }
        float TimeScale { get; }
        
        void StopTime();
        void SlowTime(float timeScale);
        void TurnBackTime();
        void SetRealTime();
    }
}
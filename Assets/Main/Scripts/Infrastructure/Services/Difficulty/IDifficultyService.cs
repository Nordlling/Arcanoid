namespace Main.Scripts.Infrastructure.Services.Difficulty
{
    public interface IDifficultyService : IService
    {
        void IncreaseDifficulty();
        float Speed { get; }
    }
}
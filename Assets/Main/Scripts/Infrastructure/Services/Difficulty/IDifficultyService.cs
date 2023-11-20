namespace Main.Scripts.Infrastructure.Services.Difficulty
{
    public interface IDifficultyService : IService
    {
        void IncreaseDifficulty(int destroyedBlocksToWin, int allBlocksToWin);
        float Speed { get; }
    }
}
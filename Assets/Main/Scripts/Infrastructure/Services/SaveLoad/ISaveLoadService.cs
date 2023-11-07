using Main.Scripts.Data;

namespace Main.Scripts.Infrastructure.Services.SaveLoad
{
  public interface ISaveLoadService : IService
  {
    void SaveProgress(PlayerScore playerScore);
    void SaveProgressImmediately(PlayerScore playerScore);
    PlayerScore LoadProgress();
  }
}
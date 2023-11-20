using Main.Scripts.Infrastructure.Services.Packs;

namespace Main.Scripts.Infrastructure.Services.SaveLoad
{
  public interface ISaveLoadService : IService
  {
    void SavePacksProgress(PacksProgress packsProgress);
    PacksProgress LoadPacksProgress();
    
    void SaveUserSettings(UserSettings userSettings);
    UserSettings LoadUserSettings();
    
    void SaveIsPlayed(int isPlayed);
    int LoadIsPlayed();
  }
}
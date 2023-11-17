using Main.Scripts.Infrastructure.Services.Packs;

namespace Main.Scripts.Infrastructure.Services.SaveLoad
{
  public interface ISaveLoadService : IService
  {
    void SavePacksProgress(PacksProgress packsProgress);
    PacksProgress LoadPacksProgress();
  }
}
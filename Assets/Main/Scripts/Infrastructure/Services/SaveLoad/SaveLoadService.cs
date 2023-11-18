using Main.Scripts.Infrastructure.Services.Packs;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private const string _packsProgressKey = "PacksProgress";
    private const string _userSettingsKey = "UserSettings";

    public void SavePacksProgress(PacksProgress packsProgress)
    {
      PlayerPrefs.SetString(_packsProgressKey, JsonUtility.ToJson(packsProgress));
      PlayerPrefs.Save();
    }

    public PacksProgress LoadPacksProgress()
    {
      return JsonUtility.FromJson<PacksProgress>(PlayerPrefs.GetString(_packsProgressKey));
    }
    
    public void SaveUserSettings(UserSettings userSettings)
    {
      PlayerPrefs.SetString(_userSettingsKey, JsonUtility.ToJson(userSettings));
      PlayerPrefs.Save();
    }

    public UserSettings LoadUserSettings()
    {
      return JsonUtility.FromJson<UserSettings>(PlayerPrefs.GetString(_userSettingsKey));
    }
  }
}
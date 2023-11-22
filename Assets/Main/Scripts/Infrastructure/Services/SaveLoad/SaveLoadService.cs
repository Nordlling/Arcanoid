using Main.Scripts.Infrastructure.Services.Energies;
using Main.Scripts.Infrastructure.Services.Packs;
using Newtonsoft.Json;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private const string _isPlayedKey = "IsPlayed";
    private const string _packsProgressKey = "PacksProgress";
    private const string _userSettingsKey = "UserSettings";
    private const string _energyKey = "Energy";

    public void SavePacksProgress(PacksProgress packsProgress)
    {
      PlayerPrefs.SetString(_packsProgressKey, JsonConvert.SerializeObject(packsProgress));
      PlayerPrefs.Save();
    }

    public PacksProgress LoadPacksProgress()
    {
      return JsonConvert.DeserializeObject<PacksProgress>(PlayerPrefs.GetString(_packsProgressKey));
    }
    
    public void SaveUserSettings(UserSettings userSettings)
    {
      PlayerPrefs.SetString(_userSettingsKey, JsonConvert.SerializeObject(userSettings));
      PlayerPrefs.Save();
    }

    public UserSettings LoadUserSettings()
    {
      return JsonConvert.DeserializeObject<UserSettings>(PlayerPrefs.GetString(_userSettingsKey));
    }
    
    public void SaveIsPlayed(int isPlayed)
    {
      PlayerPrefs.SetInt(_isPlayedKey, isPlayed);
      PlayerPrefs.Save();
    }

    public int LoadIsPlayed()
    {
      return PlayerPrefs.GetInt(_isPlayedKey);
    }

    public void SaveEnergy(EnergyData energyData)
    {
      PlayerPrefs.SetString(_energyKey, JsonConvert.SerializeObject(energyData));
      PlayerPrefs.Save();
    }

    public EnergyData LoadEnergy()
    {
      EnergyData energyData = JsonConvert.DeserializeObject<EnergyData>(PlayerPrefs.GetString(_energyKey));
      return energyData;
    }
  }
}
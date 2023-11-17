using System.Collections.Generic;
using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services.GameGrid.Loader;
using Main.Scripts.Infrastructure.Services.GameGrid.Parser;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Packs
{
    public class PackService : IPackService
    {
        private readonly ISimpleLoader _simpleLoader;
        private readonly ISimpleParser _simpleParser;
        private readonly AssetPathConfig _assetPathConfig;
        private const string _packsProgressSaveKey = "PacksProgress";

        public List<PackProgress> PackProgresses { get; private set; } = new();
        public List<PackInfo> PackInfos { get; private set; } = new();

        public int SelectedPackIndex { get; set; }
        public int WonPackIndex { get; set; }

        public PackService(AssetPathConfig assetPathConfig, ISimpleLoader simpleLoader, ISimpleParser simpleParser)
        {
            _assetPathConfig = assetPathConfig;
            _simpleLoader = simpleLoader;
            _simpleParser = simpleParser;
            InitPacks();
        }

        public string GetCurrentLevelPath()
        {
            int currentLevelIndex = PackProgresses[SelectedPackIndex].CurrentLevelIndex;
            if (currentLevelIndex >= PackInfos[SelectedPackIndex].Levels.Count)
            {
                currentLevelIndex = 0;
            }
            PackInfo selectedPackInfo = PackInfos[SelectedPackIndex];
            return $"{selectedPackInfo.LevelsPath}/{selectedPackInfo.Levels[currentLevelIndex].FileName}";
        }

        public void LevelUp()
        {
            WonPackIndex = SelectedPackIndex;
            
            int currentLevelIndex = PackProgresses[SelectedPackIndex].CurrentLevelIndex;
            int levelsCount = PackInfos[SelectedPackIndex].Levels.Count;
            
            currentLevelIndex++;
            
            if (currentLevelIndex > levelsCount)
            {
                currentLevelIndex = 1;
            }

            PackProgresses[SelectedPackIndex].CurrentLevelIndex = currentLevelIndex;
            
            if (currentLevelIndex == levelsCount)
            {
                PackProgresses[SelectedPackIndex].IsPassed = true;
                if (SelectedPackIndex + 1 < PackInfos.Count)
                {
                    PackProgresses[SelectedPackIndex + 1].IsOpen = true;
                }
                SelectedPackIndex++;
            }

            PlayerPrefs.SetString(_packsProgressSaveKey, JsonConvert.SerializeObject(new PacksProgress { Packs = PackProgresses }));
        }

        private void InitPacks()
        {
            InitPackInfos();
            InitPackProgresses();
        }

        private void InitPackInfos()
        {
            string json = _simpleLoader.LoadTextFile(_assetPathConfig.LevelPacksPath);
            if (string.IsNullOrEmpty(json))
            {
                Debug.LogWarning("No pack resources");
                return;
            }

            List<PackData> packConfigs = _simpleParser.ParseText<AllPacksData>(json).PackConfigs;
            
            for (int i = 0; i < packConfigs.Count; i++)
            {
                string packJson = _simpleLoader.LoadTextFile(packConfigs[i].PackInfoPath);
                if (string.IsNullOrEmpty(packJson))
                {
                    return;
                }

                PackInfo packInfo = _simpleParser.ParseText<PackInfo>(packJson);
                packInfo.MapImage = _simpleLoader.LoadImage(packInfo.MapImagePath);
                PackInfos.Add(packInfo);
            }
        }

        private void InitPackProgresses()
        {
            if (PackInfos.Count == 0)
            {
                return;
            }
            
            PacksProgress packsProgress = JsonConvert.DeserializeObject<PacksProgress>(PlayerPrefs.GetString(_packsProgressSaveKey));
            packsProgress = CheckForNewPacks(packsProgress);
            PackProgresses = packsProgress.Packs;
        }

        private PacksProgress CheckForNewPacks(PacksProgress packsProgress)
        {
            if (packsProgress == null)
            {
                packsProgress = new PacksProgress { Packs = new List<PackProgress>() };
            }

            for (int i = packsProgress.Packs.Count; i < PackInfos.Count; i++)
            {
                PackProgress packProgress = new PackProgress
                {
                    PackID = PackInfos[i].PackID
                };
                packsProgress.Packs.Add(packProgress);
            }

            packsProgress.Packs[0].IsOpen = true;

            PlayerPrefs.SetString(_packsProgressSaveKey, JsonConvert.SerializeObject(packsProgress));


            return packsProgress;
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services.GameGrid.Loader;
using Main.Scripts.Infrastructure.Services.GameGrid.Parser;
using Main.Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Packs
{
    public class PackService : IPackService
    {
        private readonly ISimpleLoader _simpleLoader;
        private readonly ISimpleParser _simpleParser;
        private readonly AssetPathConfig _assetPathConfig;
        private readonly ISaveLoadService _saveLoadService;

        public List<PackProgress> PackProgresses => _packsProgress.Packs;
        public List<PackInfo> PackInfos { get; private set; } = new();

        private PacksProgress _packsProgress;

        private bool _isSelected;

        public int SelectedPackIndex
        {
            get => _selectedPackIndex;
            set
            {
                _selectedPackIndex = value;
                _isSelected = true;
            }
        }

        private int _selectedPackIndex;

        public int WonPackIndex { get; private set; }
        public int WonLevelIndex { get; private set; }
        
        public event Action OnLevelUp;

        public PackService(
            AssetPathConfig assetPathConfig, 
            ISimpleLoader simpleLoader, 
            ISimpleParser simpleParser,
            ISaveLoadService saveLoadService)
        {
            _assetPathConfig = assetPathConfig;
            _simpleLoader = simpleLoader;
            _simpleParser = simpleParser;
            _saveLoadService = saveLoadService;
            InitPacks();
        }

        public string GetCurrentLevelPath()
        {
            if (!_isSelected)
            {
                return _assetPathConfig.DefaultLevelPath;
            }
            
            int currentPackLevelIndex = PackProgresses[_selectedPackIndex].CurrentLevelIndex;
            if (currentPackLevelIndex >= PackInfos[_selectedPackIndex].LevelsCount)
            {
                currentPackLevelIndex = 0;
            }
            
            int previousPacksLevelsCount = PackInfos.Take(_selectedPackIndex).Sum(pack => pack.LevelsCount);
            int currentLevelIndex = previousPacksLevelsCount + currentPackLevelIndex + 1;
            
            PackInfo selectedPackInfo = PackInfos[_selectedPackIndex];
            string resultPath = string.Format(selectedPackInfo.LevelsPathTemplate, currentLevelIndex);
            
            return resultPath;
        }

        public void LevelUp()
        {
            WonPackIndex = _selectedPackIndex;
            WonLevelIndex = PackProgresses[_selectedPackIndex].CurrentLevelIndex;

            int currentLevelIndex = WonLevelIndex;
            int levelsCount = PackInfos[_selectedPackIndex].LevelsCount;
            
            currentLevelIndex++;

            if (currentLevelIndex >= levelsCount)
            {
                currentLevelIndex = 0;
                PackUp();
            }
            
            PackProgresses[WonPackIndex].CurrentLevelIndex = currentLevelIndex;
            
            OnLevelUp?.Invoke();
            _saveLoadService.SavePacksProgress(_packsProgress);
        }

        private void PackUp()
        {
            PackProgresses[WonPackIndex].Cycle++;
            PackProgresses[WonPackIndex].IsPassed = true;
            if (_selectedPackIndex + 1 < PackInfos.Count)
            {
                _selectedPackIndex++;
                PackProgresses[_selectedPackIndex].IsOpen = true;
            }
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

                if (!ColorUtility.TryParseHtmlString(packInfo.ButtonColorRGB, out Color currentColor))
                {
                    currentColor = Color.green;
                }
                packInfo.ButtonColor = currentColor;
                
                PackInfos.Add(packInfo);
            }
        }

        private void InitPackProgresses()
        {
            if (PackInfos.Count == 0)
            {
                return;
            }
            
            _packsProgress = _saveLoadService.LoadPacksProgress();
            CheckForNewPacks();
        }

        private void CheckForNewPacks()
        {
            _packsProgress ??= new PacksProgress { Packs = new List<PackProgress>() };

            for (int i = _packsProgress.Packs.Count; i < PackInfos.Count; i++)
            {
                PackProgress packProgress = new PackProgress(PackInfos[i].PackID);
                _packsProgress.Packs.Add(packProgress);
            }

            _packsProgress.Packs[0].IsOpen = true;

            _saveLoadService.SavePacksProgress(_packsProgress);
        }
    }

}
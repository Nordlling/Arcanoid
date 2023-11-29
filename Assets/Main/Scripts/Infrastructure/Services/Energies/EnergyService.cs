using System;
using System.Threading;
using System.Threading.Tasks;
using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services.SaveLoad;
using UnityEditor;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Energies
{
    public class EnergyService : IEnergyService
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly EnergyConfig _energyConfig;

        private CancellationTokenSource _cancelToken = new();
        private const int _saveInterval = 5000;
        private int _leftSaveInterval;
        private EnergyData _energyData;

        public int PreviousEnergyCount { get; private set; }
        
        public int EnergyCapacity => _energyConfig.InitialEnergyCapacity;
        public int WasteForPlay => _energyConfig.WasteForPlay;
        public int WasteForLastTry => _energyConfig.WasteForLastTry;
        public int RewardForPass => _energyConfig.RewardForPass;
        public int RewardForBuy => _energyConfig.RewardForBuy;
        
        public int EnergyCount => _energyData.EnergyCount;
        public float CurrentSecondsToRecharge => _energyData.SecondsToRecharge;
        public float AllSecondsToRecharge => _energyConfig.SecondsForRecharge;
        
        public event Action OnEnergyChanged;

        public EnergyService(ISaveLoadService saveLoadService, EnergyConfig energyConfig)
        {
            _saveLoadService = saveLoadService;
            _energyConfig = energyConfig;

            InitEnergyData();
            InitRechargeCycle();
            InitEditorStopper();
        }

        public bool TryWasteEnergy(int energyCost)
        {
            if (energyCost < 0)
            {
                Debug.LogWarning("Negative cost");
                return false;
            }
            
            if (_energyData.EnergyCount < energyCost)
            {
                return false;
            }

            PreviousEnergyCount = EnergyCount;
            _energyData.EnergyCount -= energyCost;

            SaveWithChanges();

            return true;
        }
        
        public void RewardEnergy(int energyCost)
        {
            if (energyCost < 0)
            {
                Debug.LogWarning("Negative cost");
                return;
            }
            
            PreviousEnergyCount = EnergyCount;
            _energyData.EnergyCount = Math.Min(_energyData.EnergyCount + energyCost, _energyConfig.MaxEnergyCapacity);
            
            if (_energyData.EnergyCount >= _energyConfig.InitialEnergyCapacity)
            {
                _energyData.SecondsToRecharge = _energyConfig.SecondsForRecharge;
            }
            
            SaveWithChanges();
        }

        private void InitRechargeCycle()
        {
            _cancelToken = new();
            RechargeAsync();
            SaveIntervalAsync();
        }

        private void InitEnergyData()
        {
            _energyData = _saveLoadService.LoadEnergy() ?? new EnergyData(_energyConfig.InitialEnergyCapacity, _energyConfig.SecondsForRecharge);

            if (_energyData.EnergyCount >= _energyConfig.InitialEnergyCapacity)
            {
                return;
            }
            
            CalculateRechargeFromLastRunGame();
            PreviousEnergyCount = EnergyCount;
        }

        private void CalculateRechargeFromLastRunGame()
        {
            double seconds = (DateTime.UtcNow - _energyData.LastSaveTime).TotalSeconds;
            int accumulatedEnergyCount = (int)(seconds / _energyConfig.SecondsForRecharge);
            _energyData.EnergyCount += accumulatedEnergyCount;
            _energyData.EnergyCount = Math.Min(_energyData.EnergyCount, _energyConfig.InitialEnergyCapacity);

            if (_energyData.EnergyCount < _energyConfig.InitialEnergyCapacity)
            {
                _energyData.SecondsToRecharge -= (float)(seconds % _energyConfig.SecondsForRecharge);
            }
            else
            {
                _energyData.SecondsToRecharge = _energyConfig.SecondsForRecharge;
            }

            Save();
        }
        
        private async void SaveIntervalAsync()
        {
            while (true)
            {
                await Task.Delay(_saveInterval);
                
                if (_cancelToken.Token.IsCancellationRequested)
                {
                    break;
                }
                
                Save();
            }
        }
        
        private async void RechargeAsync()
        {
            while (true)
            {
                await Task.Delay(1000);
                
                if (_cancelToken.Token.IsCancellationRequested)
                {
                    break;
                }

                if (_energyData.EnergyCount >= _energyConfig.InitialEnergyCapacity)
                {
                    _energyData.SecondsToRecharge = 0f;
                    continue;
                }
                
                _energyData.SecondsToRecharge -= 1f;
            
                if (_energyData.SecondsToRecharge > 0)
                {
                    continue;
                }

                _energyData.SecondsToRecharge = _energyConfig.SecondsForRecharge;
            
                if (_energyData.EnergyCount + 1 > _energyConfig.InitialEnergyCapacity)
                {
                    continue;
                }

                PreviousEnergyCount = EnergyCount;
                _energyData.EnergyCount++;
                OnEnergyChanged?.Invoke();
            }
        }

        private void SaveWithChanges()
        {
            OnEnergyChanged?.Invoke();
            Save();
        }

        private void Save()
        {
            _energyData.LastSaveTime = DateTime.UtcNow;
            _saveLoadService.SaveEnergy(_energyData);
        }
        
        private void InitEditorStopper()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += (state) =>
            {
                if (state == PlayModeStateChange.ExitingPlayMode)
                {
                    _cancelToken.Cancel();
                }
            };
#endif
        }
        
    }
}
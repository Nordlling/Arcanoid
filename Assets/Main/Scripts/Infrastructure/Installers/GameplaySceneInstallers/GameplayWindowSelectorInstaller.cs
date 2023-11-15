using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class GameplayWindowSelectorInstaller : MonoInstaller
    {
        [Header("Configs")] 
        [SerializeField] private WindowsConfig _windowsConfig;
        
        [Header("Window Keys")] 
        [ValueDropdown("GetKeys")]
        [SerializeField] private string _pauseKey;
        
        [ValueDropdown("GetKeys")]
        [SerializeField] private string _gameOverKey;
        
        [ValueDropdown("GetKeys")]
        [SerializeField] private string _winKey;

        private const string _defaultKey = "Default";


        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterGameplayWindowSelector(serviceContainer);
        }

        private void RegisterGameplayWindowSelector(ServiceContainer serviceContainer)
        {
            GameplayWindowsKeysInfo gameplayWindowsKeysInfo = new GameplayWindowsKeysInfo()
            {
                DefaultKey = _defaultKey,
                PauseKey = _pauseKey,
                GameOverKey = _gameOverKey,
                WinKey = _winKey,
            };
            
            GameplayWindowSelector gameplayWindowSelector = new GameplayWindowSelector(serviceContainer.Get<IWindowsManager>(), gameplayWindowsKeysInfo);
            
            serviceContainer.SetService<IGameplayWindowSelector, GameplayWindowSelector>(gameplayWindowSelector);
            
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(gameplayWindowSelector);
        }
        
        private List<string> GetKeys()
        {
            List<string> keys = _windowsConfig.Windows.Keys.ToList();
            keys.Insert(0, _defaultKey);
            return keys;
        }
    }
}
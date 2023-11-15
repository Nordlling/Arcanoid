using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class FirstSceneWindowInstaller : MonoInstaller
    {
        [Header("Configs")] 
        [SerializeField] private WindowsConfig _windowsConfig;
        
        [ValueDropdown("GetKeys")]
        [SerializeField] private string _firstSceneKey;


        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            OpenInitialWindow(serviceContainer);
        }

        private void OpenInitialWindow(ServiceContainer serviceContainer)
        {
            IWindowsManager windowsManager = serviceContainer.Get<IWindowsManager>();
            windowsManager.GetWindow(_firstSceneKey).Open();
        }
        
        private List<string> GetKeys()
        {
            return _windowsConfig.Windows.Keys.ToList();
        }
    }
}
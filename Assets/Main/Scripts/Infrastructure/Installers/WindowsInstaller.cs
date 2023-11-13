using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.UI;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class WindowsInstaller : MonoInstaller
    {
        [Header("Configs")] 
        [SerializeField] private WindowsConfig _windowsConfig;
        
        [Header("Prefabs")] 
        [SerializeField] private WindowsManager _windowsManagerPrefab;
        [SerializeField] private Camera _cameraPrefab;
        
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterWindowsManager(serviceContainer);
        }

        private void RegisterWindowsManager(ServiceContainer serviceContainer)
        {
            Camera viewCamera = Instantiate(_cameraPrefab, transform);
            WindowsManager windowsManager = Instantiate(_windowsManagerPrefab, transform);
            windowsManager.Construct(_windowsConfig, viewCamera);
            serviceContainer.SetService<IWindowsManager, WindowsManager>(windowsManager);
        }
    }
}
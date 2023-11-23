using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.UI;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.ProjectInstallers
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
            RegisterViewCamera(serviceContainer);
            RegisterWindowsManager(serviceContainer);
        }

        private void RegisterWindowsManager(ServiceContainer serviceContainer)
        {
            WindowsManager windowsManager = Instantiate(_windowsManagerPrefab);
            windowsManager.Construct(_windowsConfig, serviceContainer.Get<Camera>());
            serviceContainer.SetService<IWindowsManager, WindowsManager>(windowsManager);
            DontDestroyOnLoad(windowsManager);
        }

        private void RegisterViewCamera(ServiceContainer serviceContainer)
        {
            Camera viewCamera = Instantiate(_cameraPrefab);
            serviceContainer.SetServiceSelf(viewCamera);
            DontDestroyOnLoad(viewCamera);
        }
    }
}
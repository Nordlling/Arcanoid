using System.Collections.Generic;
using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.UI
{
    public interface IWindowsManager
    {
        List<string> WindowKeys { get; }
        UIView GetWindow(string key);
        T GetWindow<T>() where T : UIView;
        void EnableRaycast();
        void DisableRaycast();
        void SetServiceContainer(ServiceContainer serviceContainer);
    }
}
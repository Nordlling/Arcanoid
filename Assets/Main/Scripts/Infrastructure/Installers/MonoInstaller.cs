using Main.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public abstract class MonoInstaller : MonoBehaviour
    {
        public abstract void InstallBindings(ServiceContainer serviceContainer);
    }
}
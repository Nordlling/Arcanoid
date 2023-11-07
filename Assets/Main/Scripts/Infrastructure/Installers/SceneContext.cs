using System.Collections.Generic;
using Main.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] private List<MonoInstaller> _installers = new();

        public void Setup(ServiceContainer serviceContainer)
        {
            BuildContainer(serviceContainer);
        }

        private ServiceContainer BuildContainer(ServiceContainer serviceContainer)
        {
            foreach (var installer in _installers)
            {
                installer.InstallBindings(serviceContainer);
            }

            return serviceContainer;
        }
    }
}
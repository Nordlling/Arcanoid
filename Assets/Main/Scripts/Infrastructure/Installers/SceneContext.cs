using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Main.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] private List<MonoInstaller> _installers = new();
        
        private readonly List<IInitializable> _initializables = new();
        private readonly List<ITickable> _tickables = new();

        public void Setup(ServiceContainer serviceContainer, TaskCompletionSource<bool> tcs = null)
        {
            BuildContainer(serviceContainer);
            _initializables.AddRange(serviceContainer.GetServices<IInitializable>());
            _tickables.AddRange(serviceContainer.GetServices<ITickable>());
            
            StartCoroutine(DelayStart(() => tcs?.SetResult(true)));
        }
        
        private void Update()
        {
            foreach (var tickable in _tickables)
            {
                tickable.Tick();
            }
        }

        private IEnumerator DelayStart(Action onFinished = null)
        {
            yield return null;
            Init();
            onFinished?.Invoke();
        }
        
        private void Init()
        {
            foreach (var initializable in _initializables)
            {
                initializable.Init();
            }
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
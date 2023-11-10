using Main.Scripts.Configs;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Pool;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class PoolInstaller : MonoInstaller
    {
        [Header("Configs")]
        [SerializeField] private TiledBlockConfig _tiledBlockConfig;

        [Header("Prefabs")]
        
        [Header("Scene Objects")]
        [SerializeField] private PoolProvider _blockPoolProvider;


        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterBlockFactory(serviceContainer);
        }

        private void RegisterBlockFactory(ServiceContainer serviceContainer)
        {
            _blockPoolProvider.Init();
            BasicFactory basicFactory = new BasicFactory(serviceContainer, _tiledBlockConfig, _blockPoolProvider);
            serviceContainer.SetService<IBlockFactory, BasicFactory>(basicFactory);
        }
    }
}
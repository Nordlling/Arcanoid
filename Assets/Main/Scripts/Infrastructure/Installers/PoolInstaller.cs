using Main.Scripts.Configs;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Pool;
using Main.Scripts.Test;
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
        [SerializeField] private BlockFactory _blockFactory;
        [SerializeField] private BombBlockFactory _bombBlockFactory;
        [SerializeField] private PoolProvider _boostPoolProvider;
        [SerializeField] private SpawnTest _spawnTest;


        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterFactoryContainer(serviceContainer);
            RegisterBlockFactory(serviceContainer);
        }

        private void RegisterFactoryContainer(ServiceContainer serviceContainer)
        {
            FactoryContainer factoryContainer = new FactoryContainer(_tiledBlockConfig);
            serviceContainer.SetService<IFactoryContainer, FactoryContainer>(factoryContainer);
        }

        private void RegisterBlockFactory(ServiceContainer serviceContainer)
        {
            _blockPoolProvider.Init();
            _blockFactory.Construct(_tiledBlockConfig);
            _bombBlockFactory.Construct(_tiledBlockConfig);
            serviceContainer.Get<IFactoryContainer>().AddFactory(1, _blockFactory);
            serviceContainer.Get<IFactoryContainer>().AddFactory(2, _bombBlockFactory);
        }
    }
}
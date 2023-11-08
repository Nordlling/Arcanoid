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
        [SerializeField] private PoolProvider _boostPoolProvider;
        [SerializeField] private SpawnTest _spawnTest;


        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            _blockPoolProvider.Init();
            BlockFactoryUnit blockBlockFactoryUnit = new BlockFactoryUnit(_blockPoolProvider.PoolViewUnit, _tiledBlockConfig);
            serviceContainer.SetServiceSelf(blockBlockFactoryUnit);
        }
    }
}
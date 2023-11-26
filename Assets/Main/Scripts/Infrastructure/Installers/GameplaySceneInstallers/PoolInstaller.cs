using Main.Scripts.Configs;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Pool;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class PoolInstaller : MonoInstaller
    {
        [Header("Configs")]
        [SerializeField] private TiledBlockConfig _tiledBlockConfig;
        [SerializeField] private BallsConfig _ballsConfig;

        [Header("Prefabs")]
        
        [Header("Scene Objects")]
        [SerializeField] private PoolProvider _blockPoolProvider;
        [SerializeField] private PoolProvider _ballPoolProvider;
        [SerializeField] private PoolProvider _effectPoolProvider;
        [SerializeField] private PoolProvider _boostPoolProvider;


        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterBlockFactory(serviceContainer);
            RegisterBallFactory(serviceContainer);
            RegisterEffectFactory(serviceContainer);
            RegisterBoostFactory(serviceContainer);
        }

        private void RegisterBlockFactory(ServiceContainer serviceContainer)
        {
            _blockPoolProvider.Init();
            BlockFactory blockFactory = new BlockFactory(serviceContainer, _tiledBlockConfig, _blockPoolProvider);
            serviceContainer.SetService<IBlockFactory, BlockFactory>(blockFactory);
        }
        
        private void RegisterBallFactory(ServiceContainer serviceContainer)
        {
            _ballPoolProvider.Init();
            BallFactory ballFactory = new BallFactory(serviceContainer, _ballsConfig, _ballPoolProvider);
            serviceContainer.SetService<IBallFactory, BallFactory>(ballFactory);
        }
        
        private void RegisterEffectFactory(ServiceContainer serviceContainer)
        {
            _effectPoolProvider.Init();
            EffectFactory effectFactory = new EffectFactory(serviceContainer, _effectPoolProvider);
            serviceContainer.SetService<IEffectFactory, EffectFactory>(effectFactory);
        }
        
        private void RegisterBoostFactory(ServiceContainer serviceContainer)
        {
            _boostPoolProvider.Init();
            BoostFactory boostFactory = new BoostFactory(serviceContainer, _tiledBlockConfig, _boostPoolProvider);
            serviceContainer.SetService<IBoostFactory, BoostFactory>(boostFactory);
        }
    }
}
using System.Threading.Tasks;
using Main.Scripts.Configs;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Logic.Effects;
using Main.Scripts.UI.Views;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Provides
{
    public class WinService : IWinable, IRestartable
    {
        private readonly ITimeProvider _timeProvider;
        private readonly ComprehensiveRaycastBlocker _comprehensiveRaycastBlocker;
        private readonly Vector2 _effectSpawnPosition;
        private int _currentDuration;

        private readonly Effect _effect;
        private readonly WinConfig _winConfig;

        public WinService(
            ITimeProvider timeProvider, 
            IEffectFactory effectFactory, 
            ComprehensiveRaycastBlocker comprehensiveRaycastBlocker,
            WinConfig winConfig, 
            SpawnContext spawnContext)
        {
            _timeProvider = timeProvider;
            _comprehensiveRaycastBlocker = comprehensiveRaycastBlocker;
            _winConfig = winConfig;
            
            _currentDuration = _winConfig.Duration;

            _effect = effectFactory.Spawn(spawnContext);
            _effect.EnableEffect(_winConfig.EffectKey, false);
            _effect.gameObject.SetActive(false);
        }
        
        public async Task Win()
        {
            _comprehensiveRaycastBlocker.Enable();
            _effect.gameObject.SetActive(true);
            while (_currentDuration > 0)
            {
                await Task.Delay(_winConfig.Delay);
                _currentDuration -= _winConfig.Delay;
                _timeProvider.SlowTime(_currentDuration / (float)_winConfig.Duration);
            }

            _currentDuration = _winConfig.Duration;
            _comprehensiveRaycastBlocker.Disable();
        }

        public Task Restart()
        {
            _effect.gameObject.SetActive(false);
            return Task.CompletedTask;
        }
    }
}
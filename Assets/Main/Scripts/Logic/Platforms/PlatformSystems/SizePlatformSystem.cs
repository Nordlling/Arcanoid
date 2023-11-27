using System.Threading.Tasks;
using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Provides;
using UnityEngine;

namespace Main.Scripts.Logic.Platforms.PlatformSystems
{
    public class SizePlatformSystem : ISizePlatformSystem, ITickable, IRestartable
    {
        private readonly Transform _platformTransform;
        private readonly ITimeProvider _timeProvider;
        private readonly Vector3 _initialPlatformSize;

        private bool _activated;
        private float _boostTime;
        private float _resizeSpeed;
        private SizePlatformConfig _sizePlatformConfig;

        private Vector3 _targetSize;
        private Vector3 _currentSize;


        public SizePlatformSystem(Transform platformTransform, ITimeProvider timeProvider)
        {
            _platformTransform = platformTransform;
            _timeProvider = timeProvider;
            _initialPlatformSize = _platformTransform.localScale;
            _targetSize = _initialPlatformSize;
            _currentSize = _initialPlatformSize;
        }

        public void ActivateSizeBoost(SizePlatformConfig sizePlatformConfig)
        {
            _targetSize.x = _initialPlatformSize.x * sizePlatformConfig.SizeMultiplier;
            _boostTime = sizePlatformConfig.Duration;
            _resizeSpeed = sizePlatformConfig.ResizeSpeed;
            _activated = true;
        }

        public void Tick()
        {
            _currentSize = Vector3.Lerp(_currentSize, _targetSize, _timeProvider.DeltaTime * _resizeSpeed);
            _platformTransform.localScale = _currentSize;

            if (!_activated)
            {
                return;
            }
            
            if (_boostTime <= 0f)
            {
                _targetSize = _initialPlatformSize;
                _activated = false;
                return;
            }

            _boostTime -= _timeProvider.DeltaTime;
            
        }

        public Task Restart()
        {
            _targetSize = _initialPlatformSize;
            return Task.CompletedTask;
        }
    }
}
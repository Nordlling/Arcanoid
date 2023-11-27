using System.Threading.Tasks;
using Main.Scripts.Configs;
using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Provides;

namespace Main.Scripts.Logic.Platforms.PlatformSystems
{
    public class SpeedPlatformSystem : ISpeedPlatformSystem, ITickable, IRestartable
    {
        private readonly ITimeProvider _timeProvider;
        private readonly PlatformConfig _platformConfig;

        private float _boostTime;
        private SpeedPlatformConfig _speedPlatformConfig;

        public float MovingSpeed { get; private set; }
        public float DecelerationSpeed { get; private set; }
        public float MinDistanceToMove { get; private set; }

        public SpeedPlatformSystem(ITimeProvider timeProvider, PlatformConfig platformConfig)
        {
            _timeProvider = timeProvider;
            _platformConfig = platformConfig;
        }

        public void ActivateSpeedBoost(SpeedPlatformConfig speedPlatformConfig)
        {
            _speedPlatformConfig = speedPlatformConfig;
            _boostTime = _speedPlatformConfig.Duration;
        }

        public void Tick()
        {
            MovingSpeed = _platformConfig.MovingSpeed;
            DecelerationSpeed = _platformConfig.DecelerationSpeed;
            MinDistanceToMove = _platformConfig.MinDistanceToMove;

            if (_boostTime <= 0f)
            {
                return;
            }

            _boostTime -= _timeProvider.DeltaTime;

            MovingSpeed *= _speedPlatformConfig.SpeedMultiplier;
            DecelerationSpeed *= _speedPlatformConfig.SpeedMultiplier;
            MinDistanceToMove *= _speedPlatformConfig.SpeedMultiplier;
        }

        public Task Restart()
        {
            _boostTime = 0f;
            return Task.CompletedTask;
        }
    }
}
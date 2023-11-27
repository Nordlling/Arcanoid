using System.Threading.Tasks;
using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.Balls.BallContainers;
using UnityEngine;

namespace Main.Scripts.Logic.Platforms.PlatformSystems
{
    public class MachineGunSystem : IMachineGunSystem, ITickable, IRestartable
    {
        private readonly Platform _platform;
        private readonly ITimeProvider _timeProvider;
        private readonly IBallContainer _ballContainer;

        private MachineGunConfig _machineGunConfig;
        private bool _activated;
        private float _boostTime;
        private float _fireInterval;


        public MachineGunSystem(Platform platform, ITimeProvider timeProvider, IBallContainer ballContainer)
        {
            _platform = platform;
            _timeProvider = timeProvider;
            _ballContainer = ballContainer;
        }

        public void ActivateMachineGunBoost(MachineGunConfig machineGunConfig)
        {
            _machineGunConfig = machineGunConfig;
            _boostTime = machineGunConfig.Duration;
            _fireInterval = machineGunConfig.Interval;
            _activated = true;
        }

        public void Tick()
        {
            if (!_activated)
            {
                return;
            }
            
            if (_boostTime <= 0f)
            {
                _activated = false;
                return;
            }
            
            _boostTime -= _timeProvider.DeltaTime;
            _fireInterval -= _timeProvider.DeltaTime;

            if (_fireInterval > 0f)
            {
                return;
            }

            _boostTime -= _timeProvider.DeltaTime;
            

            foreach (Transform spawnTransform in _platform.BulletSpawns)
            {
                Ball ball = _ballContainer.CreateBullet(spawnTransform.position, _machineGunConfig.BulletSpeed);
                if (ball.TryGetComponent(out Bullet bullet))
                {
                    bullet.StartMove(_machineGunConfig);
                }
            }

            _fireInterval = _machineGunConfig.Interval;

        }

        public Task Restart()
        {
            _activated = false;
            return Task.CompletedTask;
        }
    }

    public interface IMachineGunSystem
    {
        void ActivateMachineGunBoost(MachineGunConfig machineGunConfig);
    }
}
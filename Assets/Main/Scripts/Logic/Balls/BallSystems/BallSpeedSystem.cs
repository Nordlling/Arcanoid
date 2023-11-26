using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.Difficulty;
using UnityEngine;

namespace Main.Scripts.Logic.Balls.BallSystems
{
    public class BallSpeedSystem : IBallSpeedSystem, ITickable, IRestartable
    {
        private readonly IDifficultyService _difficultyService;
        private readonly ITimeProvider _timeProvider;

        private float _boostTime;
        private SpeedBallConfig _speedBallConfig;

        public float CurrentSpeed { get; private set; }

        public BallSpeedSystem(IDifficultyService difficultyService, ITimeProvider timeProvider)
        {
            _difficultyService = difficultyService;
            _timeProvider = timeProvider;
        }

        public void ActivateSpeedBoost(SpeedBallConfig speedBallConfig)
        {
            _speedBallConfig = speedBallConfig;
            _boostTime = _speedBallConfig.Duration;
        }

        public void Tick()
        {
            CurrentSpeed = _difficultyService.Speed;

            if (_boostTime <= 0f)
            {
                return;
            }

            _boostTime -= _timeProvider.DeltaTime;

            CurrentSpeed *= _speedBallConfig.SpeedMultiplier;
            CurrentSpeed = Mathf.Clamp(CurrentSpeed, _difficultyService.MinSpeed, _difficultyService.MaxSpeed);
        }

        public void Restart()
        {
            _boostTime = 0f;
        }
    }
}
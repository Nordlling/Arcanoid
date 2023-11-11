using System;
using Main.Scripts.Configs;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.Platforms;

namespace Main.Scripts.Infrastructure.Services
{
    public class HealthService : IHealthService, IRestartable
    {
        public event Action OnDecreased;
        public event Action OnIncreased;
        public event Action OnReset;
        
        private readonly IBallFactory _ballFactory;
        private readonly IGameplayStateMachine _gameplayStateMachine;
        private readonly PlatformMovement _platformMovement;
        private readonly HealthConfig _healthConfig;

        public HealthService(
            IBallFactory ballFactory, 
            IGameplayStateMachine gameplayStateMachine, 
            PlatformMovement platformMovement,
            HealthConfig healthConfig)
        {
            _ballFactory = ballFactory;
            _gameplayStateMachine = gameplayStateMachine;
            _platformMovement = platformMovement;
            _healthConfig = healthConfig;
            
            InitHealth(healthConfig);
        }

        public int LeftHealths { get; private set; }

        public bool IsMaxHealth()
        {
            return LeftHealths == _healthConfig.HealthCount;
        }

        public void DecreaseHealth()
        {
            LeftHealths--;
            OnDecreased?.Invoke();
            
            if (LeftHealths <= 0)
            {
                _gameplayStateMachine.Enter<LoseState>();
                return;
            }
            
            SpawnContext spawnContext = new SpawnContext { Parent = _platformMovement.transform };
            Ball ball = _ballFactory.Spawn(spawnContext);
            _platformMovement.InitBall(ball.BallMovement);
        }

        public void IncreaseHealth()
        {
            LeftHealths++;
            OnIncreased?.Invoke();
        }

        public void Restart()
        {
            ResetHealth();
        }

        private void ResetHealth()
        {
            LeftHealths = _healthConfig.HealthCount;
            OnReset?.Invoke();
        }

        private void InitHealth(HealthConfig healthConfig)
        {
            LeftHealths = healthConfig.HealthCount;
        }
    }
}
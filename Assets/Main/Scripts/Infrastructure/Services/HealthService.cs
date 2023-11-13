using System;
using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.GameplayStates;

namespace Main.Scripts.Infrastructure.Services
{
    public class HealthService : IHealthService, IRestartable
    {
        public event Action OnDecreased;
        public event Action OnIncreased;
        public event Action OnReset;
        
        private readonly IGameplayStateMachine _gameplayStateMachine;
        private readonly HealthConfig _healthConfig;

        public HealthService( 
            IGameplayStateMachine gameplayStateMachine,
            HealthConfig healthConfig)
        {
            _gameplayStateMachine = gameplayStateMachine;
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
            
            _gameplayStateMachine.Enter<PrePlayState>();
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
using System;
using System.Threading.Tasks;
using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.GameplayStates;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Healths
{
    public class HealthService : IHealthService, IRestartable
    {
        public event Action OnChanged;
        
        private readonly IGameplayStateMachine _gameplayStateMachine;
        private readonly HealthConfig _healthConfig;

        public int MaxHealth => _healthConfig.HealthCount;

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
            OnChanged?.Invoke();
            
            if (TryLose())
            {
                return;
            }
            
            _gameplayStateMachine.Enter<PrePlayState>();
        }

        public void IncreaseHealth()
        {
            if (LeftHealths >= _healthConfig.HealthCount)
            {
                return;
            }
            LeftHealths++;
            OnChanged?.Invoke();
        }

        public void ChangeHealth(int count, bool canDie)
        {
            LeftHealths += count;
            if (canDie && TryLose())
            {
                return;
            }

            LeftHealths = Mathf.Clamp(LeftHealths, 0, _healthConfig.HealthCount);
            OnChanged?.Invoke();
        }

        public Task Restart()
        {
            ResetHealth();
            return Task.CompletedTask;
        }

        private bool TryLose()
        {
            if (LeftHealths >= 0)
            {
                return false;
            }
            _gameplayStateMachine.Enter<LoseState>();
            return true;

        }

        private void ResetHealth()
        {
            LeftHealths = _healthConfig.HealthCount;
            OnChanged?.Invoke();
        }

        private void InitHealth(HealthConfig healthConfig)
        {
            LeftHealths = healthConfig.HealthCount;
        }
    }
}
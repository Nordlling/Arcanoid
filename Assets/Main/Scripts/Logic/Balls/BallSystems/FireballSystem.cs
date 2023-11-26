using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.GameGrid;
using Main.Scripts.Logic.Balls.BallContainers;

namespace Main.Scripts.Logic.Balls.BallSystems
{
    public class FireballSystem : IFireballSystem, ITickable, IRestartable
    {
        private readonly ITimeProvider _timeProvider;
        private readonly IGameGridService _gameGridService;
        private readonly IBallContainer _ballContainer;

        private bool _activated;
        private float _boostTime;

        public FireballSystem(ITimeProvider timeProvider, IGameGridService gameGridService, IBallContainer ballContainer)
        {
            _gameGridService = gameGridService;
            _timeProvider = timeProvider;
            _ballContainer = ballContainer;
        }

        public void ActivateFireballBoost(FireballConfig fireballConfig)
        {
            _activated = true;
            _boostTime = fireballConfig.Duration;
            _gameGridService.EnableTriggerForAllBlocks();
            _ballContainer.FireAllBalls();
        }

        public void Tick()
        {
            if (!_activated)
            {
                return;
            }
            
            if (_boostTime <= 0f)
            {
                _activated = true;
                _gameGridService.DisableTriggerForAllBlocks();
                _ballContainer.UnfireAllBalls();
                return;
            }

            _boostTime -= _timeProvider.DeltaTime;
        }

        public void Restart()
        {
            _boostTime = 0f;
        }
        
    }
}
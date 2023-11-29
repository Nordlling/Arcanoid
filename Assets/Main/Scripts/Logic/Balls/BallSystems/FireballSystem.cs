using System.Threading.Tasks;
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
        private readonly IGameGridController _gameGridController;
        private readonly IBallContainer _ballContainer;

        private bool _activated;
        private float _boostTime;

        public FireballSystem(ITimeProvider timeProvider, IGameGridController gameGridController, IBallContainer ballContainer)
        {
            _gameGridController = gameGridController;
            _timeProvider = timeProvider;
            _ballContainer = ballContainer;
        }

        public void ActivateFireballBoost(FireballConfig fireballConfig)
        {
            _activated = true;
            _boostTime = fireballConfig.Duration;
            _gameGridController.EnableTriggerForAllBlocks();
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
                _activated = false;
                _gameGridController.DisableTriggerForAllBlocks();
                _ballContainer.UnfireAllBalls();
                return;
            }

            _boostTime -= _timeProvider.DeltaTime;
        }

        public Task Restart()
        {
            _boostTime = 0f;
            _activated = false;
            return Task.CompletedTask;
        }
        
    }
}
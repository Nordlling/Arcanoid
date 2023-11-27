using System.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Logic.Zones;
using UnityEngine;

namespace Main.Scripts.Logic.Balls.BallContainers
{
    public class BallKeeper : ITickable, IPrePlayable, IPlayable
    {
        private readonly ZonesManager _zonesManager;
        private readonly Camera _camera;
        private readonly ITimeProvider _timeProvider;
        private readonly IGameplayStateMachine _gameplayStateMachine;
        
        public Ball Ball { get; set; }
        private bool _isPrePlay;

        public BallKeeper(
            ZonesManager zonesManager, 
            Camera viewCamera, 
            ITimeProvider timeProvider, 
            IGameplayStateMachine gameplayStateMachine
            )
        {
            _zonesManager = zonesManager;
            _camera = viewCamera;
            _timeProvider = timeProvider;
            _gameplayStateMachine = gameplayStateMachine;
        }

        public Task PrePlay()
        {
            _isPrePlay = true;
            return Task.CompletedTask;
        }

        public Task Play()
        {
            _isPrePlay = false;
            TryStartBallMove();
            return Task.CompletedTask;
        }

        public void Tick()
        {
            if (!_isPrePlay || _timeProvider.Stopped)
            {
                return;
            }

            if (!_zonesManager.IsInInputZone(_camera.ScreenToWorldPoint(Input.mousePosition)))
            {
                return;
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                _gameplayStateMachine.Enter<PlayState>();
            }
        }

        private void TryStartBallMove()
        {
            if (Ball == null)
            {
                return;
            }
            
            if (!Ball.TryGetComponent(out BallMovement ballMovement))
            {
                return;
            }
            
            ballMovement.transform.parent = null;
            ballMovement.StartMove();
            Ball = null;
        }
    }
}
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Logic.GameGrid;
using UnityEngine;

namespace Main.Scripts.Logic.Platforms
{
    public class BallKeeper : ITickable, IPrePlayable, IPlayable
    {
        private readonly ZonesManager _zonesManager;
        private readonly Camera _camera;
        private readonly ITimeProvider _timeProvider;
        private readonly IGameplayStateMachine _gameplayStateMachine;
        
        private BallMovement _ball;
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

        public void InitBall(BallMovement ball)
        {
            _ball = ball;
        }

        public void PrePlay()
        {
            _isPrePlay = true;
        }

        public void Play()
        {
            _isPrePlay = false;
            TryStartBallMove();
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
            if (_ball == null)
            {
                return;
            }
            
            _ball.transform.parent = null;
            _ball.StartMove();
            _ball = null;
        }
    }
}
using System;
using System.Collections.Generic;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Logic.Platforms;

namespace Main.Scripts.Logic.Balls
{
    public class BallManager : IBallManager, IPrePlayable, ILoseable, IWinable, IRestartable
    {
        private readonly PlatformMovement _platformMovement;
        private readonly IBallFactory _ballFactory;
        private readonly BallKeeper _ballKeeper;

        private readonly List<Ball> _balls = new();

        public BallManager(PlatformMovement platformMovement, IBallFactory ballFactory, BallKeeper ballKeeper)
        {
            _platformMovement = platformMovement;
            _ballFactory = ballFactory;
            _ballKeeper = ballKeeper;
        }

        private void CreateBall()
        {
            SpawnContext spawnContext = new SpawnContext { Parent = _platformMovement.transform };
            Ball ball = _ballFactory.Spawn(spawnContext);
            ball.transform.position = _platformMovement.BallPoint.position;
            _balls.Add(ball);
            _ballKeeper.InitBall(ball.BallMovement);
        }

        public void RemoveBall(Ball ball)
        {
            _balls.Remove(ball);
            _ballFactory.Despawn(ball);
        }


        public void PrePlay()
        {
            CreateBall();
        }

        public void Lose()
        {
            foreach (Ball ball in _balls)
            {
                ball.BallMovement.Stop = true;
            }
        }

        public void Win()
        {
            ClearAllBalls();
        }

        public void Restart()
        {
            ClearAllBalls();
        }

        private void ClearAllBalls()
        {
            foreach (Ball ball in _balls)
            {
                _ballFactory.Despawn(ball);
            }
        }
    }
}
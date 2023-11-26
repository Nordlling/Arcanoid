using System.Collections.Generic;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.Healths;
using Main.Scripts.Logic.Platforms;

namespace Main.Scripts.Logic.Balls.BallContainers
{
    public class BallContainer : IBallContainer, IPrePlayable, ILoseable, IWinable, IRestartable
    {
        private readonly PlatformMovement _platformMovement;
        private readonly IBallFactory _ballFactory;
        private readonly BallKeeper _ballKeeper;
        private readonly IHealthService _healthService;

        private bool isFireball;

        public List<Ball> Balls { get; private set; } = new();

        public BallContainer(
            PlatformMovement platformMovement, 
            IBallFactory ballFactory, 
            BallKeeper ballKeeper,
            IHealthService healthService)
        {
            _platformMovement = platformMovement;
            _ballFactory = ballFactory;
            _ballKeeper = ballKeeper;
            _healthService = healthService;
        }

        public void CreateBall()
        {
            if (_ballKeeper.Ball is not null)
            {
                return;
            }
            SpawnContext spawnContext = new SpawnContext { Parent = _platformMovement.transform };
            Ball ball = _ballFactory.Spawn(spawnContext);
            ball.transform.position = _platformMovement.BallPoint.position;
            if (isFireball)
            {
                ball.Fireball.EnableVisual();
            }
            Balls.Add(ball);
            _ballKeeper.Ball = ball.BallMovement;
        }

        public void RemoveBall(Ball ball)
        {
            Balls.Remove(ball);
            _ballFactory.Despawn(ball);

            if (Balls.Count <= 0)
            {
                _healthService.DecreaseHealth();
            }
        }
        
        public void FireAllBalls()
        {
            isFireball = true;
            foreach (Ball ball in Balls)
            {
                ball.Fireball.EnableVisual();
            }
        }
        
        public void UnfireAllBalls()
        {
            isFireball = false;
            foreach (Ball ball in Balls)
            {
                ball.Fireball.DisableVisual();
            }
        }

        public void Lose()
        {
            foreach (Ball ball in Balls)
            {
                ball.BallMovement.Stop = true;
            }
        }

        public void PrePlay()
        {
            CreateBall();
        }

        public void Win()
        {
            isFireball = false;
            ClearAllBalls();
        }

        public void Restart()
        {
            isFireball = false;
            ClearAllBalls();
        }

        private void ClearAllBalls()
        {
            foreach (Ball ball in Balls)
            {
                _ballFactory.Despawn(ball);
            }
        }
    }
}
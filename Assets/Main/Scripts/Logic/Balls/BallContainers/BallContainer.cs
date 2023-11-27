using System;
using System.Collections.Generic;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.Healths;
using Main.Scripts.Logic.Platforms;
using UnityEngine;

namespace Main.Scripts.Logic.Balls.BallContainers
{
    public class BallContainer : IBallContainer, IPrePlayable, IRestartable
    {
        private readonly PlatformMovement _platformMovement;
        private readonly IBallFactory _ballFactory;
        private readonly BallKeeper _ballKeeper;
        private readonly IHealthService _healthService;

        private bool isFireball;

        private readonly SpawnContext _spawnContext = new();
        private const string _ballKey = "Ball";
        private const string _bulletKey = "Bullet";

        public event Action<bool> OnSwitchedFireball;
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

        public void CreateBallOnPlatform()
        {
            if (_ballKeeper.Ball is not null)
            {
                return;
            }

            _spawnContext.ID = _ballKey;
            _spawnContext.Position = Vector2.zero;
            _spawnContext.Parent = _platformMovement.transform;
            Ball ball = _ballFactory.Spawn(_spawnContext);
            ball.transform.position = _platformMovement.BallPoint.position;
            
            OnSwitchedFireball?.Invoke(isFireball);

            _ballKeeper.Ball = ball;
            
            Balls.Add(ball);
        }
        
        public Ball CreateBall(Vector2 position, float leftAngle, float rightAngle)
        {
            _spawnContext.ID = _ballKey;
            _spawnContext.Position = position;
            _spawnContext.Parent = null;
            Ball ball = _ballFactory.Spawn(_spawnContext);
            
            OnSwitchedFireball?.Invoke(isFireball);
            
            if (ball.TryGetComponent(out BallMovement ballMovement))
            {
                ballMovement.StartMove(leftAngle, rightAngle);
            }
            
            Balls.Add(ball);
            return ball;
        }
        
        public Ball CreateBullet(Vector2 position, float speed)
        {
            _spawnContext.ID = _bulletKey;
            _spawnContext.Position = position;
            _spawnContext.Parent = null;
            Ball ball = _ballFactory.Spawn(_spawnContext);
            
            Balls.Add(ball);
           
            return ball;
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
            OnSwitchedFireball?.Invoke(isFireball);
        }
        
        public void UnfireAllBalls()
        {
            isFireball = false;
            OnSwitchedFireball?.Invoke(isFireball);
        }

        public void PrePlay()
        {
            CreateBallOnPlatform();
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
            Balls.Clear();
        }
    }
}
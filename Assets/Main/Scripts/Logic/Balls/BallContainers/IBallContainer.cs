using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Logic.Balls.BallContainers
{
    public interface IBallContainer
    {
        List<Ball> Balls { get; }
        
        void CreateBallOnPlatform();
        Ball CreateBall(Vector2 position, float leftAngle, float rightAngle);
        void RemoveBall(Ball ball);
        void FireAllBalls();
        void UnfireAllBalls();
        event Action<bool> OnSwitchedFireball;
    }
}
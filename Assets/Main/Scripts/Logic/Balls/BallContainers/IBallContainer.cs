using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Logic.Balls.BallContainers
{
    public interface IBallContainer
    {
        List<Ball> Balls { get; }
        
        void CreateBallOnPlatform();
        void CreateBall(Vector2 position);
        void RemoveBall(Ball ball);
        void FireAllBalls();
        void UnfireAllBalls();
    }
}
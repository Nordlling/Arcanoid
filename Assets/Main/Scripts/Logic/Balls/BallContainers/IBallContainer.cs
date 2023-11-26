using System.Collections.Generic;

namespace Main.Scripts.Logic.Balls.BallContainers
{
    public interface IBallContainer
    {
        void CreateBall();
        void RemoveBall(Ball ball);
        List<Ball> Balls { get; }
        void FireAllBalls();
        void UnfireAllBalls();
    }
}
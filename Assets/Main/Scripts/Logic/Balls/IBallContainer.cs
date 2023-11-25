using System.Collections.Generic;

namespace Main.Scripts.Logic.Balls
{
    public interface IBallContainer
    {
        void CreateBall();
        void RemoveBall(Ball ball);
        List<Ball> Balls { get; }
    }
}
using Main.Scripts.Logic.Balls.BallContainers;
using UnityEngine;

namespace Main.Scripts.Logic.Balls.BallSystems
{
    public class ExtraBallSystem : IExtraBallSystem
    {
        private readonly IBallContainer _ballContainer;

        public ExtraBallSystem(IBallContainer ballContainer)
        {
            _ballContainer = ballContainer;
        }

        public void ActivateExtraBallBoost(Vector2 position)
        {
            _ballContainer.CreateBall(position, 180, 180);
        }
        
    }

    public interface IExtraBallSystem
    {
        void ActivateExtraBallBoost(Vector2 position);
    }
}
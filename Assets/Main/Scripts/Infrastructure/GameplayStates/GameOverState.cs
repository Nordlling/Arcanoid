using System.Collections.Generic;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class GameOverState : IGameplayState
    {
        private readonly List<IGameOverable> _gameOverables = new();
        
        public void AddStatable(IGameplayStatable gameplayStatable)
        {
            if (gameplayStatable is IGameOverable overable)
            {
                _gameOverables.Add(overable); 
            }
        }

        public void Enter()
        {
            foreach (IGameOverable gameOverable in _gameOverables)
            {
                gameOverable.GameOver();
            }
        }

        public void Exit()
        {
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IGameOverable : IGameplayStatable
    {
        void GameOver();
    }
}
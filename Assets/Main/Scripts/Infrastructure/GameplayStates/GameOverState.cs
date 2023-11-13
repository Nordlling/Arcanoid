using System.Collections.Generic;
using Main.Scripts.UI;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class GameOverState : IGameplayState
    {
        private readonly List<IGameOverable> _gameOverables = new();
        private readonly IWindowsManager _windowsManager;

        public GameOverState(IWindowsManager windowsManager)
        {
            _windowsManager = windowsManager;
        }
        
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

            _windowsManager.GetWindow<GameOverUIView>().Open();
        }

        public void Exit()
        {
            _windowsManager.GetWindow<GameOverUIView>().Close();
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IGameOverable : IGameplayStatable
    {
        void GameOver();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task Enter()
        {
            foreach (IGameOverable gameOverable in _gameOverables)
            {
                await gameOverable.GameOver();
            }
        }

        public Task Exit()
        {
            return Task.CompletedTask;
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IGameOverable : IGameplayStatable
    {
        Task GameOver();
    }
}
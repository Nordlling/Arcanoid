using System.Collections.Generic;
using System.Threading.Tasks;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class RestartState : IGameplayState
    {
        private readonly List<IRestartable> _restartables = new();
        
        public void AddStatable(IGameplayStatable gameplayStatable)
        {
            if (gameplayStatable is IRestartable restartable)
            {
                _restartables.Add(restartable); 
            }
        }

        public async Task Enter()
        {
            foreach (IRestartable restartable in _restartables)
            {
                await restartable.Restart();
            }
            await StateMachine.Enter<PrepareState>();
        }

        public Task Exit()
        {
            return Task.CompletedTask;
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IRestartable : IGameplayStatable
    {
        Task Restart();
    }
}
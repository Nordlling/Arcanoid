using System.Collections.Generic;
using System.Threading.Tasks;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class PrePlayState : IGameplayState
    {
        private readonly List<IPrePlayable> _prePlayables = new();

        public void AddStatable(IGameplayStatable gameplayStatable)
        {
            if (gameplayStatable is IPrePlayable prePlayable)
            {
                _prePlayables.Add(prePlayable); 
            }
        }

        public async Task Enter()
        {
            foreach (IPrePlayable restartable in _prePlayables)
            {
                await restartable.PrePlay();
            }
        }

        public Task Exit()
        {
            return Task.CompletedTask;
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IPrePlayable : IGameplayStatable
    {
        Task PrePlay();
    }
}
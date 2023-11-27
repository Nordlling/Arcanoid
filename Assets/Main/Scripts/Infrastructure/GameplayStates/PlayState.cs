using System.Collections.Generic;
using System.Threading.Tasks;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class PlayState : IGameplayState
    {
        private readonly List<IPlayable> _playables = new();
        
        public GameplayStateMachine StateMachine { get; set; }
        
        public void AddStatable(IGameplayStatable gameplayStatable)
        {
            if (gameplayStatable is IPlayable playable)
            {
                _playables.Add(playable); 
            }
        }

        public async Task Enter()
        {
            foreach (IPlayable playable in _playables)
            {
                await playable.Play();
            }
        }

        public Task Exit()
        {
            return Task.CompletedTask;
        }
    }

    public interface IPlayable : IGameplayStatable
    {
        Task Play();
    }
}
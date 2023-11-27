using System.Collections.Generic;
using System.Threading.Tasks;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class PauseState : IGameplayState
    {
        private List<IPauseable> _pauseables = new();
        
        public void AddStatable(IGameplayStatable gameplayStatable)
        {
            if (gameplayStatable is IPauseable pauseable)
            {
                _pauseables.Add(pauseable); 
            }
        }

        public async Task Enter()
        {
            foreach (IPauseable pauseable in _pauseables)
            {
                await pauseable.Pause();
            }
        }

        public async Task Exit()
        {
            foreach (IPauseable pauseable in _pauseables)
            {
                await pauseable.UnPause();
            }
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IPauseable : IGameplayStatable
    {
        Task Pause();
        Task UnPause();
    }
}
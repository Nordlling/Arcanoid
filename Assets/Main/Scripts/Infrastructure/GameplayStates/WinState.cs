using System.Collections.Generic;
using System.Threading.Tasks;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class WinState : IGameplayState
    {
        private readonly List<IWinable> _winables = new();
        
        public void AddStatable(IGameplayStatable gameplayStatable)
        {
            if (gameplayStatable is IWinable winable)
            {
                _winables.Add(winable); 
            }
        }

        public async Task Enter()
        {
            foreach (IWinable winable in _winables)
            {
                await winable.Win();
            }
        }

        public Task Exit()
        {
            return Task.CompletedTask;
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IWinable : IGameplayStatable
    {
        Task Win();
    }
}
using System.Collections.Generic;

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

        public void Enter()
        {
            foreach (IWinable winable in _winables)
            {
                winable.Win();
            }
        }

        public void Exit()
        {
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IWinable : IGameplayStatable
    {
        void Win();
    }
}
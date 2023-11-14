using System.Collections.Generic;

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

        public void Enter()
        {
            foreach (IPauseable pauseable in _pauseables)
            {
                pauseable.Pause();
            }
        }

        public void Exit()
        {
            foreach (IPauseable pauseable in _pauseables)
            {
                pauseable.UnPause();
            }
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IPauseable : IGameplayStatable
    {
        void Pause();
        void UnPause();
    }
}
using System.Collections.Generic;
using Main.Scripts.Infrastructure.Provides;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class PauseState : IGameplayState
    {
        private readonly ITimeProvider _timeProvider;
        private List<IPauseable> _pauseables = new();


        public PauseState(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }
        
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

            _timeProvider.StopTime();;
        }

        public void Exit()
        {
            _timeProvider.TurnBackTime();
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IPauseable : IGameplayStatable
    {
        void Pause();
    }
}
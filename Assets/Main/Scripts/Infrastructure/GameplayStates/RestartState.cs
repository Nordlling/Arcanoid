using System.Collections.Generic;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class RestartState : IGameplayState
    {
        private List<IRestartable> _restartables = new();
        
        public void AddStatable(IGameplayStatable gameplayStatable)
        {
            if (gameplayStatable is IRestartable restartable)
            {
                _restartables.Add(restartable); 
            }
        }

        public void Enter()
        {
            foreach (IRestartable restartable in _restartables)
            {
                restartable.Restart();
            }
            StateMachine.Enter<PrepareState>();
        }

        public void Exit()
        {
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IRestartable : IGameplayStatable
    {
        void Restart();
    }
}
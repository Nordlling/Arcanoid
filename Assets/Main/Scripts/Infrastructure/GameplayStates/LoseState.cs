using System.Collections.Generic;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class LoseState : IGameplayState
    {
        private readonly List<ILoseable> _loseables = new();

        public void AddStatable(IGameplayStatable gameplayStatable)
        {
            if (gameplayStatable is ILoseable loseable)
            {
                _loseables.Add(loseable); 
            }
        }

        public void Enter()
        {
            foreach (ILoseable loseable in _loseables)
            {
                loseable.Lose();
            }
            StateMachine.Enter<GameOverState>();
        }

        public void Exit()
        {
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface ILoseable : IGameplayStatable
    {
        void Lose();
    }
}
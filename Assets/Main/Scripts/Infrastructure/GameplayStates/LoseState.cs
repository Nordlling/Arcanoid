using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task Enter()
        {
            foreach (ILoseable loseable in _loseables)
            {
                await loseable.Lose();
            }
            StateMachine.Enter<GameOverState>();
        }

        public Task Exit()
        {
            return Task.CompletedTask;
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface ILoseable : IGameplayStatable
    {
        Task Lose();
    }
}
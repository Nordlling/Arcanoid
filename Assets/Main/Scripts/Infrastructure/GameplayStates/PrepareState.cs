using System.Collections.Generic;
using System.Threading.Tasks;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class PrepareState : IGameplayState
    {
        private readonly List<IPreparable> _preparables = new();

        public void AddStatable(IGameplayStatable gameplayStatable)
        {
            if (gameplayStatable is IPreparable preparable)
            {
                _preparables.Add(preparable); 
            }
        }

        public async Task Enter()
        {
            foreach (IPreparable restartable in _preparables)
            {
                await restartable.Prepare();
            }
            await StateMachine.Enter<PrePlayState>();
        }

        public Task Exit()
        {
            return Task.CompletedTask;
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IPreparable : IGameplayStatable
    {
        Task Prepare();
    }
}
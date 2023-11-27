using System.Threading.Tasks;
using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public interface IGameplayStateMachine : IService
    {
        void AddState(IGameplayState state);
        void AddGameplayStatable(IGameplayStatable gameplayStatable);
        Task Enter<TState>() where TState : class, IGameplayState;
        Task EnterPreviousState();
        bool IsSameState<TState>() where TState : class, IGameplayState;
    }
}
using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public interface IGameplayStateMachine : IService
    {
        void AddState(IGameplayState state);
        void AddGameplayStatable(IGameplayStatable gameplayStatable);
        void Enter<TState>() where TState : class, IGameplayState;
    }
}
using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Infrastructure.States
{
    public interface IGameStateMachine : IService
    {
        void AddState(IExitableState state);
        
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TParameter>(TParameter payload) where TState : class, IParametrizedState<TParameter>;
    }
}
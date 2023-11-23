using System.Threading.Tasks;
using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Infrastructure.States
{
    public interface IGameStateMachine : IService
    {
        void AddState(IExitableState state);
        
        Task Enter<TState>() where TState : class, IState;
        Task Enter<TState, TParam1>(TParam1 param1) where TState : class, IParametrizedState<TParam1>;
        Task Enter<TState, TParam1, TParam2>(TParam1 param1, TParam2 param2) where TState : class, IParametrizedState<TParam1, TParam2>;
    }
}
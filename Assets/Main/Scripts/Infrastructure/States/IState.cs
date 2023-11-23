using System.Threading.Tasks;

namespace Main.Scripts.Infrastructure.States
{
    public interface IState : IExitableState
    {
        Task Enter();
    }
    
    public interface IParametrizedState<TParam1> : IExitableState
    {
        Task Enter(TParam1 param);
    }
    
    public interface IParametrizedState<TParam1, TParam2> : IExitableState
    {
        Task Enter(TParam1 param1, TParam2 param2);
    }

    public interface IExitableState
    {
        Task Exit();
        
        GameStateMachine StateMachine { get; set; }
    }
}
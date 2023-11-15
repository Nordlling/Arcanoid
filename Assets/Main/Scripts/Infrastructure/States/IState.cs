namespace Main.Scripts.Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
    
    public interface IParametrizedState<TParam1> : IExitableState
    {
        void Enter(TParam1 param);
    }
    
    public interface IParametrizedState<TParam1, TParam2> : IExitableState
    {
        void Enter(TParam1 param1, TParam2 param2);
    }

    public interface IExitableState
    {
        void Exit();
        
        GameStateMachine StateMachine { get; set; }
    }
}
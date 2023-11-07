namespace Main.Scripts.Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
    
    public interface IParametrizedState<TParameter> : IExitableState
    {
        void Enter(TParameter param);
    }

    public interface IExitableState
    {
        void Exit();
        
        GameStateMachine StateMachine { get; set; }
    }
}
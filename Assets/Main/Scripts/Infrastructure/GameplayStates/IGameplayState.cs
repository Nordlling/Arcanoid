namespace Main.Scripts.Infrastructure.GameplayStates
{
    public interface IGameplayState
    {
        void AddStatable(IGameplayStatable gameplayStatable);
        void Enter();
        void Exit();
        GameplayStateMachine StateMachine { get; set; }
    }
}
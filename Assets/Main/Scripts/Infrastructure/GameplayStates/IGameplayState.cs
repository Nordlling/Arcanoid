using System.Threading.Tasks;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public interface IGameplayState
    {
        void AddStatable(IGameplayStatable gameplayStatable);
        Task Enter();
        Task Exit();
        GameplayStateMachine StateMachine { get; set; }
    }
}
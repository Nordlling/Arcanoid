using System.Collections.Generic;
using Main.Scripts.UI;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class PrepareState : IGameplayState
    {
        private readonly IWindowsManager _windowsManager;
        private readonly List<IPreparable> _preparables = new();

        public PrepareState(IWindowsManager windowsManager)
        {
            _windowsManager = windowsManager;
        }

        public void AddStatable(IGameplayStatable gameplayStatable)
        {
            if (gameplayStatable is IPreparable preparable)
            {
                _preparables.Add(preparable); 
            }
        }

        public void Enter()
        {
            foreach (IPreparable restartable in _preparables)
            {
                restartable.Prepare();
            }
        }

        public void Exit()
        {
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IPreparable : IGameplayStatable
    {
        void Prepare();
    }
}
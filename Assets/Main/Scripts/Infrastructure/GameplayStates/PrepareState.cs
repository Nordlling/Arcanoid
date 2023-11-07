using System.Collections.Generic;
using Main.Scripts.Infrastructure.Services.ButtonContainer;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class PrepareState : IGameplayState
    {
        private readonly IButtonContainerService _buttonContainerService;
        private List<IPreparable> _preparables = new();

        public PrepareState(IButtonContainerService buttonContainerService)
        {
            _buttonContainerService = buttonContainerService;
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
            _buttonContainerService.DisableAllButtons();
        }

        public void Exit()
        {
            _buttonContainerService.EnableAllButtons();
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IPreparable : IGameplayStatable
    {
        void Prepare();
    }
}
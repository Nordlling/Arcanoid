using System.Collections.Generic;
using Main.Scripts.Infrastructure.Services.ButtonContainer;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class LoseState : IGameplayState
    {
        private readonly IButtonContainerService _buttonContainerService;
        private List<ILoseable> _loseables = new();

        public LoseState(IButtonContainerService buttonContainerService)
        {
            _buttonContainerService = buttonContainerService;
        }

        public void AddStatable(IGameplayStatable gameplayStatable)
        {
            if (gameplayStatable is ILoseable loseable)
            {
                _loseables.Add(loseable); 
            }
        }

        public void Enter()
        {
            foreach (ILoseable loseable in _loseables)
            {
                loseable.Lose();
            }
            // _buttonContainerService.DisableAllButtons();
        }

        public void Exit()
        {
            // _buttonContainerService.EnableAllButtons();
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface ILoseable : IGameplayStatable
    {
        void Lose();
    }
}
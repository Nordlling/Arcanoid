using System.Collections.Generic;
using Main.Scripts.UI;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class PrePlayState : IGameplayState
    {
        private readonly List<IPrePlayable> _prePlayables = new();

        public void AddStatable(IGameplayStatable gameplayStatable)
        {
            if (gameplayStatable is IPrePlayable prePlayable)
            {
                _prePlayables.Add(prePlayable); 
            }
        }

        public void Enter()
        {
            foreach (IPrePlayable restartable in _prePlayables)
            {
                restartable.PrePlay();
            }
        }

        public void Exit()
        {
        }

        public GameplayStateMachine StateMachine { get; set; }
    }

    public interface IPrePlayable : IGameplayStatable
    {
        void PrePlay();
    }
}
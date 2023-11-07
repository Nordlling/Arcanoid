using System.Collections.Generic;

namespace Main.Scripts.Infrastructure.GameplayStates
{
    public class PlayState : IGameplayState
    {
        private List<IPlayable> _playables = new();
        
        public GameplayStateMachine StateMachine { get; set; }
        
        public void AddStatable(IGameplayStatable gameplayStatable)
        {
            if (gameplayStatable is IPlayable playable)
            {
                _playables.Add(playable); 
            }
        }

        public void Enter()
        {
            foreach (IPlayable playable in _playables)
            {
                playable.Play();
            }
        }

        public void Exit()
        {
        }
    }

    public interface IPlayable : IGameplayStatable
    {
        void Play();
    }
}
using System;
using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.GameplayStates;

namespace Main.Scripts.Infrastructure.Services.Difficulty
{
    public class DifficultyService : IDifficultyService, IRestartable
    {
        public float Speed { get; private set; }
        
        private readonly DifficultyConfig _difficultyConfig;

        public DifficultyService(DifficultyConfig difficultyConfig)
        {
            _difficultyConfig = difficultyConfig;
            ResetDifficulty();
        }

        public void IncreaseDifficulty()
        {
            Speed += _difficultyConfig.BallSpeedIncrement;
            Speed = Math.Clamp(Speed, 0f, _difficultyConfig.BallMaxSpeed);
        }

        public void Restart()
        {
            ResetDifficulty();
        }

        private void ResetDifficulty()
        {
            Speed = _difficultyConfig.BallSpeedInit;
        }
    }
}
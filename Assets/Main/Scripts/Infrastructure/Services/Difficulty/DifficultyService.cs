using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.GameplayStates;
using UnityEngine;

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

        public void IncreaseDifficulty(int destroyedBlocksToWin, int allBlocksToWin)
        {
            if (allBlocksToWin == 0)
            {
                return;
            }

            float interpolation = destroyedBlocksToWin / (float)allBlocksToWin;
            Speed = Mathf.Lerp(_difficultyConfig.BallSpeedInit, _difficultyConfig.BallMaxSpeed, interpolation);
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
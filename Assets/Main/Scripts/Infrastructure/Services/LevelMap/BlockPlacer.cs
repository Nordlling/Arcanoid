using Main.Scripts.Configs;
using Main.Scripts.Factory;
using Main.Scripts.LevelMap;
using Main.Scripts.Logic.GameGrid;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.LevelMap
{
    public class BlockPlacer
    {
        private readonly GameGridZone _gameGridZone;
        private readonly GameGridConfig _gameGridConfig;
        private readonly IFactoryUnit _blockFactoryUnit;
        
        private Vector2 _blockWithSpacingScale;
        private Vector2 _blockWithSpacingSize;
        private Vector2 _blockScale;
        private Vector2 _firstBlockPosition;

        public BlockPlacer(IFactoryUnit blockFactoryUnit, GameGridZone gameGridZone, GameGridConfig gameGridConfig)
        {
            _blockFactoryUnit = blockFactoryUnit;
            _gameGridZone = gameGridZone;
            _gameGridConfig = gameGridConfig;
        }

        public void SpawnGrid(LevelMapInfo levelMapInfo)
        {
            CalculateBlockSize(levelMapInfo.Width);
            
            for (int y = 0; y < levelMapInfo.Height; y++)
            {
                for (int x = 0; x < levelMapInfo.Width; x++)
                {
                    if (levelMapInfo.LevelMap[x, y] == 0)
                    {
                        continue;
                    }
                    Vector2 spawnPosition = _firstBlockPosition + _blockWithSpacingSize * new Vector2(x, -y);
                    BlockSpawnContext blockSpawnContext = new BlockSpawnContext { SpawnPosition = spawnPosition, BlockID = levelMapInfo.LevelMap[x, y]};
                    var block = _blockFactoryUnit.Spawn(blockSpawnContext);
                    if (block != null)
                    {
                        block.transform.localScale = new Vector3(_blockScale.x, _blockScale.y, 1f);
                    }

                } 
            }
        }

        private void CalculateBlockSize(int gridWidth)
        {
            Rect gameGridRect = _gameGridZone.GameGridRect;
            Vector2 spriteSize = _gameGridConfig.BlockSprite.bounds.size;
            float aspectRatio = spriteSize.y / spriteSize.x;
            float blockWithSpacingWidth = gameGridRect.width / gridWidth;
            float spriteRatio = blockWithSpacingWidth / spriteSize.x;
            
            _blockWithSpacingSize = new Vector2(blockWithSpacingWidth, blockWithSpacingWidth * aspectRatio);
            _blockWithSpacingScale = new Vector2(spriteRatio, spriteRatio);
            _blockScale = _blockWithSpacingScale - new Vector2(_gameGridConfig.Spacing * 2f, _gameGridConfig.Spacing * 2f);
            _firstBlockPosition = new Vector2(gameGridRect.xMin + _blockWithSpacingSize.x * 0.5f, gameGridRect.yMax - _blockWithSpacingSize.y * 0.5f);
        }
    }
}
using Main.Scripts.Configs;
using Main.Scripts.Factory;
using Main.Scripts.GameGrid;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.GameGrid;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.GameGrid
{
    public class BlockPlacer
    {
        private readonly IBlockFactory _blockFactory;
        private readonly ZonesManager _zonesManager;
        private readonly GameGridConfig _gameGridConfig;
        
        private Vector2 _blockWithSpacingScale;
        private Vector2 _blockWithSpacingSize;
        private Vector2 _blockScale;
        private Vector2 _firstBlockPosition;

        public BlockPlacer(IBlockFactory blockFactory, ZonesManager zonesManager, GameGridConfig gameGridConfig)
        {
            _blockFactory = blockFactory;
            _zonesManager = zonesManager;
            _gameGridConfig = gameGridConfig;
        }

        public BlockPlaceInfo[,] SpawnGrid(GameGridInfo gameGridInfo)
        {
            BlockPlaceInfo[,] blocks = new BlockPlaceInfo[gameGridInfo.Size.x, gameGridInfo.Size.y];
            
            CalculateBlockSize(gameGridInfo.Size.x);
            
            for (int y = 0; y < gameGridInfo.Size.y; y++)
            {
                for (int x = 0; x < gameGridInfo.Size.x; x++)
                {
                    bool checkToWin = false;
                    if (gameGridInfo.LevelMap[x, y] == 0)
                    {
                        blocks[x, y] = new BlockPlaceInfo(gameGridInfo.LevelMap[x, y], null, checkToWin);
                        continue;
                    }
                    
                    Vector2 spawnPosition = _firstBlockPosition + _blockWithSpacingSize * new Vector2(x, -y);
                    SpawnContext spawnContext = new SpawnContext { ID = gameGridInfo.LevelMap[x, y].ToString(), Position = spawnPosition};
                    
                    var block = _blockFactory.Spawn(spawnContext);
                    
                    if (block != null)
                    {
                        block.transform.localScale = new Vector3(_blockScale.x, _blockScale.y, 1f);
                        block.GridPosition = new Vector2Int(x, y);
                        checkToWin = block.TryGetComponent(out Health _);
                    }

                    blocks[x, y] = new BlockPlaceInfo(gameGridInfo.LevelMap[x, y], block, checkToWin);

                } 
            }

            return blocks;
        }

        private void CalculateBlockSize(int gridWidth)
        {
            Rect gameGridRect = _zonesManager.GameGridRect;
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
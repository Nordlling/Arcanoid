using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Boosts;
using Main.Scripts.UI.Views;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.GameGrid
{
    public class GameGridController : IGameGridController, IPreparable, IRestartable, IPrePlayable
    {
        private readonly IBlockFactory _blockFactory;
        private readonly BlockPlacer _blockPlacer;
        private readonly IGameplayStateMachine _gameplayStateMachine;
        private readonly ComprehensiveRaycastBlocker _comprehensiveRaycastBlocker;

        private readonly GameGridService _gameGridService;

        public GameGridController(
            GameGridService gameGridService,
            IGameplayStateMachine gameplayStateMachine,
            ComprehensiveRaycastBlocker comprehensiveRaycastBlocker)
        {
            _gameGridService = gameGridService;
            _gameplayStateMachine = gameplayStateMachine;
            _comprehensiveRaycastBlocker = comprehensiveRaycastBlocker;
        }

        public void EnableTriggerForAllBlocks()
        {
            SwitchTriggerForAllBlocks(true);
        }
        
        public void DisableTriggerForAllBlocks()
        {
            SwitchTriggerForAllBlocks(false);
        }
        
        public async Task KillAllWinnableBlocks(float time)
        {
            float seconds = time / _gameGridService.AllBlocksToWin;
            int interval = (int)(seconds * 1000);
            
            for (int y = 0; y < _gameGridService.GridSize.y; y++)
            {
                for (int x = 0; x < _gameGridService.GridSize.x; x++)
                {
                    if (_gameGridService.CurrentLevel[x, y].Block == null || !_gameGridService.CurrentLevel[x, y].CheckToWin)
                    {
                        continue;
                    }
                    await KillBlock(_gameGridService.CurrentLevel[x, y].Block, interval);
                }
            }
        }

        private async Task KillBlock(Block block, int interval)
        {
            if (block.TryGetComponent(out Health health))
            {
                health.TakeDamage(999);
            }

            if (block.TryGetComponent(out BoostKeeper boostKeeper))
            {
                boostKeeper.Interact();
            }
            
            if (block.TryGetComponent(out Explosion explosion))
            {
                explosion.Interact();
            }
            
            if (block.TryGetComponent(out ExtraBall extraBall))
            {
                extraBall.Interact();
            }

            await Task.Delay(interval);
        }

        private void SwitchTriggerForAllBlocks(bool enabled)
        {
            for (int y = 0; y < _gameGridService.GridSize.y; y++)
            {
                for (int x = 0; x < _gameGridService.GridSize.x; x++)
                {
                    if (_gameGridService.CurrentLevel[x, y].Block != null)
                    {
                        _gameGridService.CurrentLevel[x, y].Block.Collider.isTrigger = enabled;
                    }
                }
            }
        }

        public async Task Prepare()
        {
            _comprehensiveRaycastBlocker.Enable();
            await ShowAllBlocks(1);
        }

        public Task PrePlay()
        {
            _comprehensiveRaycastBlocker.Disable();
            return Task.CompletedTask;
        }

        public async Task Restart()
        {
            _comprehensiveRaycastBlocker.Enable();
            await HideAllBlocks(1f);
            _gameGridService.RestartLevel();
        }

        private async Task HideAllBlocks(float time)
        {
            float interval = time / _gameGridService.AllBlocks;
            
            for (int y = 0; y < _gameGridService.GridSize.y; y++)
            {
                for (int x = 0; x < _gameGridService.GridSize.x; x++)
                {
                    BlockPlaceInfo blockPlaceInfo = _gameGridService.CurrentLevel[x, y];
                    if (blockPlaceInfo.Block is null)
                    {
                        continue;
                    }
                    
                    await _gameGridService.CurrentLevel[x, y].Block.transform.DOScale(Vector2.zero, interval);
                }
            }
        }

        private async Task ShowAllBlocks(float time)
        {
            float interval = time / _gameGridService.AllBlocks;
            
            for (int y = 0; y < _gameGridService.GridSize.y; y++)
            {
                for (int x = 0; x < _gameGridService.GridSize.x; x++)
                {
                    BlockPlaceInfo blockPlaceInfo = _gameGridService.CurrentLevel[x, y];
                    if (blockPlaceInfo.Block is null)
                    {
                        continue;
                    }
                    
                    await _gameGridService.CurrentLevel[x, y].Block.transform.DOScale(blockPlaceInfo.Size, interval);
                }
            }
        }
        
    }
}
using System.Collections.Generic;
using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Services.GameGrid;

namespace Main.Scripts.Logic.Explosions
{
    public class ExplosionInfo
    {
        public readonly ExplosionConfig ExplosionConfig;
        public float LeftTime;
        public readonly CellInfo RootCell;
        public List<CellInfo> CurrentCells;

        public ExplosionInfo(ExplosionConfig explosionConfig, CellInfo rootCell)
        {
            ExplosionConfig = explosionConfig;
            RootCell = rootCell;
            CurrentCells = new List<CellInfo> { rootCell };
        }
    }

    public class CellInfo
    {
        public readonly BlockPlaceInfo BlockPlaceInfo;
        public readonly CellInfo Parent;
        public readonly List<CellInfo> Childrens;

        public CellInfo(BlockPlaceInfo blockPlaceInfo, CellInfo parent)
        {
            BlockPlaceInfo = blockPlaceInfo;
            Parent = parent;
            Childrens = new();
        }
    }
}
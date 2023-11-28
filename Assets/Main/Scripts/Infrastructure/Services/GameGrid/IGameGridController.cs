using System.Threading.Tasks;

namespace Main.Scripts.Infrastructure.Services.GameGrid
{
    public interface IGameGridController
    {
        void EnableTriggerForAllBlocks();
        void DisableTriggerForAllBlocks();
        Task KillAllWinnableBlocks(float time);
    }
}
using System.Threading.Tasks;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Logic.Balls.BallContainers;
using Main.Scripts.Logic.Zones;

namespace Main.Scripts.Logic.Balls
{
    public class BallBoundsChecker : ITickable, IPrePlayable, IPlayable
    {
        private readonly ZonesManager _zonesManager;
        private readonly IBallContainer _ballContainer;

        public bool Check { get; set; } = true;

        public BallBoundsChecker(ZonesManager zonesManager, IBallContainer ballContainer)
        {
            _zonesManager = zonesManager;
            _ballContainer = ballContainer;
        }

        public void Tick()
        {
            if (!Check)
            {
                return;
            }
            
            for (int i = 0; i < _ballContainer.Balls.Count; i++)
            {
                if (!_zonesManager.IsInLivingZone(_ballContainer.Balls[i].transform.position))
                {
                    _ballContainer.RemoveBall(_ballContainer.Balls[i]);
                }
            }
        }

        public Task PrePlay()
        {
            Check = true;
            return Task.CompletedTask;
        }

        public Task Play()
        {
            Check = true;
            return Task.CompletedTask;
        }
    }
}
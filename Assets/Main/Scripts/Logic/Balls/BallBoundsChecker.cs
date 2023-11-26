using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Logic.Balls.BallContainers;
using Main.Scripts.Logic.Zones;

namespace Main.Scripts.Logic.Balls
{
    public class BallBoundsChecker : ITickable
    {
        private readonly ZonesManager _zonesManager;
        private readonly IBallContainer _ballContainer;

        public BallBoundsChecker(ZonesManager zonesManager, IBallContainer ballContainer)
        {
            _zonesManager = zonesManager;
            _ballContainer = ballContainer;
        }

        public void Tick()
        {
            for (int i = 0; i < _ballContainer.Balls.Count; i++)
            {
                if (!_zonesManager.IsInLivingZone(_ballContainer.Balls[i].transform.position))
                {
                    _ballContainer.RemoveBall(_ballContainer.Balls[i]);
                }
            }
        }
    }
}
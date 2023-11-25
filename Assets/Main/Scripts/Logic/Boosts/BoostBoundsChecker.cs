using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Logic.Zones;

namespace Main.Scripts.Logic.Boosts
{
    public class BoostBoundsChecker : ITickable
    {
        private readonly ZonesManager _zonesManager;
        private readonly IBoostContainer _boostContainer;

        public BoostBoundsChecker(ZonesManager zonesManager, IBoostContainer boostContainer)
        {
            _zonesManager = zonesManager;
            _boostContainer = boostContainer;
        }

        public void Tick()
        {
            for (int i = 0; i < _boostContainer.Boosts.Count; i++)
            {
                if (!_zonesManager.IsInLivingZone(_boostContainer.Boosts[i].transform.position))
                {
                    _boostContainer.RemoveBoost(_boostContainer.Boosts[i]);
                }
            }
        }
    }
}
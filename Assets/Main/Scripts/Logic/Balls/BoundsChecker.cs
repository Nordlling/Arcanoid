using System;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.GameGrid;
using UnityEngine;

namespace Main.Scripts.Logic.Balls
{
    public class BoundsChecker : MonoBehaviour, IDieable
    {
        private ZonesManager _zonesManager;

        public event Action OnDied;

        public void Construct(ZonesManager zonesManager)
        {
            _zonesManager = zonesManager;
        }

        private void Update()
        {
            if (!_zonesManager.IsInLivingZone(transform.position))
            {
                OnDied?.Invoke();
            }
        }
    }
}
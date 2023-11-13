using System;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.GameGrid;
using UnityEngine;

namespace Main.Scripts.Logic.Balls
{
    public class BoundsChecker : MonoBehaviour, IDieable
    {
        private ZonesManager _zonesManager;
        private IHealthService _healthService;

        public event Action OnDied;

        public void Construct(ZonesManager zonesManager, IHealthService healthService)
        {
            _healthService = healthService;
            _zonesManager = zonesManager;
        }

        private void Update()
        {
            if (!_zonesManager.IsInLivingZone(transform.position))
            {
                OnDied?.Invoke();
                _healthService.DecreaseHealth();
            }
        }
    }
}
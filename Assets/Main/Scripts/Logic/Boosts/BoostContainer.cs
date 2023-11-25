using System.Collections.Generic;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using UnityEngine;

namespace Main.Scripts.Logic.Boosts
{
    public class BoostContainer : IBoostContainer, IRestartable
    {
        private readonly IBoostFactory _boostFactory;
        private readonly SpawnContext _spawnContext = new();

        public List<Boost> Boosts { get; private set; } = new();

        public BoostContainer(IBoostFactory boostFactory)
        {
            _boostFactory = boostFactory;
        }

        public void CreateBoost(string ID, Vector2 position)
        {
            _spawnContext.ID = ID;
            _spawnContext.Position = position;
            Boost boost = _boostFactory.Spawn(_spawnContext);
            Boosts.Add(boost);
        }

        public void RemoveBoost(Boost boost)
        {
            Boosts.Remove(boost);
            _boostFactory.Despawn(boost);
        }

        public void Restart()
        {
            ClearAllBoosts();
        }

        private void ClearAllBoosts()
        {
            foreach (Boost boost in Boosts)
            {
                _boostFactory.Despawn(boost);
            }
            Boosts.Clear();
        }
    }
}
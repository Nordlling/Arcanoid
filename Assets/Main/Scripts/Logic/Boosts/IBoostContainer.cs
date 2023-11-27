using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Logic.Boosts
{
    public interface IBoostContainer
    {
        List<Boost> Boosts { get; }
        Boost CreateBoost(string ID, Vector2 position);
        void RemoveBoost(Boost boost);
    }
}
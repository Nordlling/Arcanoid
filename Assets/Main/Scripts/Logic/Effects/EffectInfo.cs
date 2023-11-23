using System;
using UnityEngine;

namespace Main.Scripts.Logic.Effects
{
    [Serializable]
    public class EffectInfo
    {
        public string Key;
        public ParticleSystem Effect;
        public ParticleSystem[] Particles;
    }
}
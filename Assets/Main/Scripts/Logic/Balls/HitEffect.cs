using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.Services.Collision;
using UnityEngine;

namespace Main.Scripts.Logic.Balls
{
    public class HitEffect : MonoBehaviour, ICollisionInteractable
    {
        private IEffectFactory _effectFactory;
        private string _effectKey;

        public void Construct(IEffectFactory effectFactory, string effectKey)
        {
            _effectKey = effectKey;
            _effectFactory = effectFactory;
        }

        public void Interact()
        {
            _effectFactory.SpawnAndEnable(transform.position, _effectKey);
        }
    }
}
using Main.Scripts.Infrastructure.Services.Collision;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Explosion : MonoBehaviour, ICollisionInteractable
    {
        public void Interact()
        {
            transform.localScale *= 0.5f;
        }
    }
}
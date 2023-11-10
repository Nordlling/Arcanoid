using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Explosion : MonoBehaviour
    {
        public void Explode()
        {
            transform.localScale *= 0.5f;
        }
    }
}
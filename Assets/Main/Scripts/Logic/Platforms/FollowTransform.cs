using UnityEngine;

namespace Main.Scripts.Logic.Platforms
{
    public class FollowTransform : MonoBehaviour
    {
        [SerializeField] private Transform _followed;

        private void Update()
        {
            transform.position = _followed.position;
        }
    }
}
using UnityEngine;

namespace Main.Scripts.Logic.Platforms
{
    public class Platform : MonoBehaviour
    {
        public Transform StretchedTransform => _stretchedTransform;
        public PlatformMovement PlatformMovement => _platformMovement;
        public Transform[] BulletSpawns => _bulletSpawns;

        [SerializeField] private PlatformMovement _platformMovement;
        [SerializeField] private Transform _stretchedTransform;
        [SerializeField] private Transform[] _bulletSpawns;
    }
}
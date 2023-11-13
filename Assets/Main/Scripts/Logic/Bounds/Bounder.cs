using UnityEngine;

namespace Main.Scripts.Logic.Bounds
{
    public class Bounder : MonoBehaviour
    {
        public BoundInfo[] Bounders => _bounders;
        [SerializeField] private BoundInfo[] _bounders;
        [SerializeField] private GameObject _boundAreaPrefab;
        
        public void Init()
        {
            foreach (BoundInfo boundInfo in _bounders)
            {
                GameObject bounder = Instantiate(_boundAreaPrefab, transform);
                bounder.transform.position = boundInfo.CenterPoint;
                bounder.transform.localScale = boundInfo.Size;
            }
        }
    }
}
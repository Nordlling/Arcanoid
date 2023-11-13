using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Logic.Bounds
{
    public class Bounder : MonoBehaviour
    {
        public BoundInfo[] BoundInfos => boundInfos;
        [SerializeField] private BoundInfo[] boundInfos;
        [SerializeField] private GameObject _boundAreaPrefab;

        private readonly List<GameObject> _bounders = new();
        
        public void Init()
        {
            for (int i = 0; i < boundInfos.Length; i++)
            {
                _bounders.Add(Instantiate(_boundAreaPrefab, transform));
            }
        }

        public void RelocateBounders()
        {
            for (int i = 0; i < _bounders.Count; i++)
            {
                _bounders[i].transform.position = boundInfos[i].CenterPoint;
                _bounders[i].transform.localScale = boundInfos[i].Size;
            }
        }
    }
}
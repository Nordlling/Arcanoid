using System;
using UnityEngine;

namespace Main.Scripts.Logic.Bounds
{
    public class Bounder : MonoBehaviour
    {
        public BoundInfo[] Bounders => _bounders;
        [SerializeField] private BoundInfo[] _bounders;
        [SerializeField] private GameObject _boundAreaPrefab;


        private void Start()
        {
            foreach (BoundInfo boundInfo in _bounders)
            {
                GameObject bounder = Instantiate(_boundAreaPrefab, transform);
                bounder.transform.position = boundInfo.CenterPoint;
                bounder.transform.localScale = boundInfo.Size;
            }
        }
    }
    
    [Serializable]
    public class BoundInfo
    {
        [Range(-1, 2)]
        public float CenterX;
        [Range(-1, 2)]
        public float CenterY;
        [Range(0, 1)]
        public float Width;
        [Range(0, 1)]
        public float Height;
        
        [HideInInspector]
        public Vector2 CenterPoint;
        [HideInInspector]
        public Vector2 Size;
    }
    
}
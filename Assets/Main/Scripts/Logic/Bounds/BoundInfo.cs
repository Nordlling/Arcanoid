using System;
using UnityEngine;

namespace Main.Scripts.Logic.Bounds
{
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
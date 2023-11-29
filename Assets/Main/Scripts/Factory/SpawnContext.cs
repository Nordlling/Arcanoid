using UnityEngine;

namespace Main.Scripts.Factory
{
    public class SpawnContext
    {
        public string ID;
        public Vector2 Position;
        public Transform Parent;
        public Vector3 Scale = Vector3.one;

        public void Reset()
        {
            ID = null;
            Position = Vector2.zero;
            Parent = null;
            Scale = Vector3.one;
        }
    }
    
}
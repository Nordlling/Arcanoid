using UnityEngine;

namespace Main.Scripts.Factory
{
    public abstract class UnitSpawnContext
    {
          
    }
    
    public class BoostSpawnContext : UnitSpawnContext
    {
        public Vector3 SpawnPosition;
        public Quaternion SpawnRotation;
    }
    
    public class BlockSpawnContext : UnitSpawnContext
    {
        public Vector3 SpawnPosition;
    }
    
    
}
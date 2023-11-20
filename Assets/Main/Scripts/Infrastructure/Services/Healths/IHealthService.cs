using System;

namespace Main.Scripts.Infrastructure.Services.Healths
{
    public interface IHealthService : IService
    {
        int LeftHealths { get; }
        
        event Action OnDecreased;
        event Action OnIncreased;
        event Action OnReset;
        
        bool IsMaxHealth();
        void DecreaseHealth();
        void IncreaseHealth();
    }
}
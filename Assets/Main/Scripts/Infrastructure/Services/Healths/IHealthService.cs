using System;

namespace Main.Scripts.Infrastructure.Services.Healths
{
    public interface IHealthService : IService
    {
        int LeftHealths { get; }
        int MaxHealth { get; }

        event Action OnChanged;

        bool IsMaxHealth();
        void DecreaseHealth();
        void IncreaseHealth();
        void ChangeHealth(int count, bool canDie);
    }
}
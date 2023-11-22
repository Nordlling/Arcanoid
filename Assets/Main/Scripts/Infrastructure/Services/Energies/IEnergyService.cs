using System;

namespace Main.Scripts.Infrastructure.Services.Energies
{
    public interface IEnergyService : IService
    {
        bool TryWasteEnergy(int energyCost);
        void RewardEnergy();

        int PreviousEnergyCount { get; }
        int EnergyCount { get; }
        int EnergyCapacity { get; }
        int EnergyForPlay { get; }
        int EnergyForLastTry { get; }

        event Action OnEnergyChanged;
    }
}
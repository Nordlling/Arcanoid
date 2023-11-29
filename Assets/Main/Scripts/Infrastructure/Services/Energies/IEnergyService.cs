using System;

namespace Main.Scripts.Infrastructure.Services.Energies
{
    public interface IEnergyService : IService
    {
        bool TryWasteEnergy(int energyCost);
        void RewardEnergy(int energyCost);

        int PreviousEnergyCount { get; }
        int EnergyCapacity { get; }
        int WasteForPlay { get; }
        int WasteForLastTry { get; }
        int RewardForPass { get; }
        int RewardForBuy { get; }
        int EnergyCount { get; }
        float CurrentSecondsToRecharge { get; }
        float AllSecondsToRecharge { get; }


        event Action OnEnergyChanged;
    }
}
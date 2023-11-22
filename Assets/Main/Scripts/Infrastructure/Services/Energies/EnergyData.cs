using System;

namespace Main.Scripts.Infrastructure.Services.Energies
{
    [Serializable]
    public class EnergyData
    {
        public int EnergyCount;
        public float SecondsToRecharge;
        public DateTime LastSaveTime;

        public EnergyData(int energyCount, float secondsToRecharge)
        {
            EnergyCount = energyCount;
            SecondsToRecharge = secondsToRecharge;
        }
    }
}
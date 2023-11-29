using System.Collections.Generic;
using Main.Scripts.Configs;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.BoostTimers
{
    public class BoostTimersService : IBoostTimersService
    {
        public Dictionary<string, Sprite> Icons { get; private set; } = new();
        public List<ITimerBoost> TimerBoosts { get; private set; } = new();

        public BoostTimersService(TiledBlockConfig tiledBlockConfig)
        {
            foreach (var kvp in tiledBlockConfig.BoostInfos)
            {
                Icons[kvp.Key] = kvp.Value.BasicInfo.Visual;
            }
        }
        
    }
}
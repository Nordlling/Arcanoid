using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.BoostTimers
{
    public interface IBoostTimersService
    {
        Dictionary<string, Sprite> Icons { get; }
        List<ITimerBoost> TimerBoosts { get; }
    }
}
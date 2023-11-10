using System;

namespace Main.Scripts.Logic.Blocks
{
    public interface IDieable
    {
        event Action OnDied;
    }
}
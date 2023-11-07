using System;

namespace Main.Scripts.Infrastructure.Services.Applications
{
    public interface IApplicationService : IService
    {
        event Action OnPaused;
        event Action OnSaved;
    }
}
using Main.Scripts.Infrastructure.Services;

namespace Main.Scripts.Infrastructure.Starters
{
    public interface ISceneStarter
    {
        void StartScene(ServiceContainer serviceContainer);
    }
}
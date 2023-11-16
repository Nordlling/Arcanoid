namespace Main.Scripts.Infrastructure.Services.LevelMap.Parser
{
    public interface ISimpleParser
    {
        T ParseText<T>(string json) where T : class;
    }
}
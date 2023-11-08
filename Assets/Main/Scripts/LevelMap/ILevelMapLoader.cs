using Main.Scripts.Data;

namespace Main.Scripts.LevelMap
{
    public interface ILevelMapLoader
    {
        public string LoadLevelMap(string path, CurrentLevelInfo currentLevelInfo);
    }
}
using Main.Scripts.Data;

namespace Main.Scripts.LevelMap
{
    public interface ILevelMapParser
    {
        LevelMapInfo ParseLevelMap(string textData);
    }
}
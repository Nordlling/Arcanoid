namespace Main.Scripts.GameGrid
{
    public interface IGameGridParser
    {
        LevelMapInfo ParseLevelMap(string textData);
    }
}
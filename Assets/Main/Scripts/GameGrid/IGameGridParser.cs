namespace Main.Scripts.GameGrid
{
    public interface IGameGridParser
    {
        GameGridInfo ParseLevelMap(string textData);
    }
}
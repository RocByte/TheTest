namespace BattleshipsGameConsole.View.Output;

public class PlayerBoard
{
    public PlayerBoard(int size)
    {
        Size = size;
        GridValues = new string?[size, size];
    }
    
    public int Size { get; }
    public string?[,] GridValues { get; }
}
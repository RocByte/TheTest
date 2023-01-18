namespace BattleshipsGame.Core.Contract;

public record Cell(int RowIndex, int ColumnIndex)
{
    public int ColumnIndex { get; } = ColumnIndex;
    public int RowIndex { get; } = RowIndex;
}
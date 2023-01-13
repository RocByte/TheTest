using BattleshipsGame.Core.Contract;

namespace BattleshipsGame.Common;

public class SquareGridAreaValidator
{
    public int SquareGridSize { get; }

    public SquareGridAreaValidator(int squareGridSize)
    {
        SquareGridSize = squareGridSize;
    }

    public bool FitsInSquareGrid(Cell newShipCell)
    {
        return newShipCell.RowIndex >= 0
               && newShipCell.ColumnIndex >= 0
               && newShipCell.RowIndex < SquareGridSize
               && newShipCell.ColumnIndex < SquareGridSize;
    }
}
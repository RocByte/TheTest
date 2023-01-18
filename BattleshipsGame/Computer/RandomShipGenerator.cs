using BattleshipsGame.Computer.Contract;
using BattleshipsGame.Core.Contract;

namespace BattleshipsGame.Computer;

public class RandomShipGenerator : IRandomShipGenerator
{
    private readonly IRandomGenerator randomGenerator;

    public RandomShipGenerator(IRandomGenerator randomGenerator)
    {
        this.randomGenerator = randomGenerator;
    }

    public Ship GetRandomShip(int maxFirstCellIndex, int shipLenght)
    {
        var firstCell = randomGenerator.GetRandomCell(maxFirstCellIndex);
        return new Ship(GetShipCells(firstCell, shipLenght));
    }

    private IList<Cell> GetShipCells(Cell firstCell, int shipLenght)
    {
        return randomGenerator.GetRandomOrientation() == ShipOrientation.Vertical 
            ? GetVerticalShipCells(firstCell, shipLenght) 
            : GetHorizontalShipCells(firstCell, shipLenght);
    }

    private static IList<Cell> GetHorizontalShipCells(Cell firstCell, int shipLenght)
    {
        return Enumerable
            .Range(firstCell.ColumnIndex, shipLenght)
            .Select(currentColumnIndex => new Cell(firstCell.RowIndex, currentColumnIndex))
            .ToList();
    }

    private static IList<Cell> GetVerticalShipCells(Cell firstCell, int shipLenght)
    {
        return Enumerable
            .Range(firstCell.RowIndex, shipLenght)
            .Select(currentRowIndex => new Cell(currentRowIndex, firstCell.ColumnIndex))
            .ToList();
    }
}
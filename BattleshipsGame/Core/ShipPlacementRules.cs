using BattleshipsGame.Common;
using BattleshipsGame.Core.Contract;

namespace BattleshipsGame.Core;

public class ShipPlacementRules
{
    private readonly SquareGridAreaValidator squareGridAreaValidator;
    public int SquareGridSize => squareGridAreaValidator.SquareGridSize;

    public ShipPlacementRules(SquareGridAreaValidator squareGridAreaValidator)
    {
        this.squareGridAreaValidator = squareGridAreaValidator;
    }

    public bool CanAddShip(Ship newShip, IList<Ship> otherShips)
    {
        return newShip.Cells
            .All(newShipCell => squareGridAreaValidator.FitsInSquareGrid(newShipCell)
                                && OverlapsWithOtherShip(newShipCell, otherShips) == false);
    }

    private static bool OverlapsWithOtherShip(Cell newShipCell, IEnumerable<Ship> otherShips)
    {
        return otherShips
            .Any(otherShip => otherShip.Cells
                .Any(otherShipCell => otherShipCell.Equals(newShipCell)));
    }
}
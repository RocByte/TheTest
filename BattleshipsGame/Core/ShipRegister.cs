using BattleshipsGame.Core.Contract;

namespace BattleshipsGame.Core;

public class ShipRegister : IShipRegister
{
    public IList<Ship> AllShips { get; } = new List<Ship>();

    public void AddShip(Ship ship)
    {
        AllShips.Add(ship);
    }
    
    public bool AnyShipLeft()
    {
        return AllShips.Any(ship => ship.Alive);
    }

    public ShotResult Shoot(Cell targetCell)
    {
        var targetShip = TryGetShip(targetCell);
        if (targetShip == null)
            return ShotResult.Miss; 
        
        targetShip.RegisterHit();

        return targetShip.Alive 
            ? ShotResult.Hit 
            : ShotResult.Sunk;
    }

    public Ship GetShip(Cell cell)
    {
        return TryGetShip(cell)
               ?? throw new ArgumentOutOfRangeException(
                   nameof(cell), 
                   cell, 
                   "Invalid cell request");
    }

    private Ship? TryGetShip(Cell cell)
    {
        return AllShips
            .FirstOrDefault(ship => ship.Cells
                .Any(shipCell => shipCell.Equals(cell)));
    }
}
namespace BattleshipsGame.Core.Contract;

public interface IShipRegister
{
    IList<Ship> AllShips { get; }
    ShotResult Shoot(Cell targetCell);
    Ship GetShip(Cell cell);
}
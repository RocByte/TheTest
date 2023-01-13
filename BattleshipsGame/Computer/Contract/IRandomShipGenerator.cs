using BattleshipsGame.Core.Contract;

namespace BattleshipsGame.Computer.Contract;

public interface IRandomShipGenerator
{
    Ship GetRandomShip(int maxFirstCellIndex, int shipLenght);
}
using BattleshipsGame.Core.Contract;

namespace BattleshipsGame.Computer.Contract;

public interface IRandomGenerator
{
    Cell GetRandomCell(int maxIndex);
    ShipOrientation GetRandomOrientation();
}
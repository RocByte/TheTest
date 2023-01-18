using BattleshipsGame.Computer.Contract;
using BattleshipsGame.Core.Contract;

namespace BattleshipsGame.Computer;

public class RandomGenerator : IRandomGenerator
{
    private readonly Random random;

    public RandomGenerator()
    {
        random = new Random();
    }

    public Cell GetRandomCell(int maxIndex)
    {
        var exclusiveMaxValue = maxIndex + 1;
        return new Cell(
            RowIndex: random.Next(0, exclusiveMaxValue),
            ColumnIndex: random.Next(0, exclusiveMaxValue));
    }
    
    public ShipOrientation GetRandomOrientation()
    {
        return random.Next(1, 11) <= 5 
            ? ShipOrientation.Vertical
            : ShipOrientation.Horizontal;
    }
}
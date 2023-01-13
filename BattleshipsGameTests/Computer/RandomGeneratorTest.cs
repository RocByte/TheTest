using BattleshipsGame.Computer;
using BattleshipsGame.Computer.Contract;
using BattleshipsGame.Core.Contract;

namespace BattleshipsGameTests.Computer;

public class RandomGeneratorTest
{
    private RandomGenerator testedRandomGenerator = null!;

    [SetUp]
    public void SetUp()
    {
        testedRandomGenerator = new RandomGenerator();
    }

    [Test(Description = "May fail but it's very unlikely")]
    public void CanGenerateAllPossibleShipOrientationsTest()
    {
        var allExpectedShipOrientations = new List<ShipOrientation>
        {
            ShipOrientation.Vertical,
            ShipOrientation.Horizontal
        };

        AllRandomValuesUsedTest(
            attemptsCount: 100,
            allExpectedShipOrientations,
            getRandomValue: () => testedRandomGenerator.GetRandomOrientation());
    }
    
    [Test(Description = "May fail but it's very unlikely")]
    public void CanGenerateAllPossibleCellsTest()
    {
        var allExpectedCells = new List<Cell>
        {
            new Cell(0, 0),
            new Cell(1, 0),
            new Cell(0, 1),
            new Cell(1, 1)
        };
        
        AllRandomValuesUsedTest(
            attemptsCount: 100,
            allExpectedCells,
            getRandomValue: () => testedRandomGenerator.GetRandomCell(maxIndex: 1));
    }
    
    private static void AllRandomValuesUsedTest<T>(
        int attemptsCount,
        ICollection<T> expectedValues,
        Func<T> getRandomValue)
    {
        for (var i = 0; i < attemptsCount; i++)
        {
            var currentValue = getRandomValue();
            
            if (expectedValues.Contains(currentValue))
                expectedValues.Remove(currentValue);
            
            if (expectedValues.Count == 0)
                break;
        }
        
        Assert.That(expectedValues, Is.Empty);
    }
}
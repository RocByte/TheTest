using BattleshipsGame.Computer;
using BattleshipsGame.Computer.Contract;
using BattleshipsGame.Core.Contract;

namespace BattleshipsGameTests.Computer;

public class RandomShipGeneratorTest
{
    private RandomShipGenerator testedShipGenerator = null!;
    private IRandomGenerator randomGeneratorMock = null!;

    [SetUp]
    public void SetUp()
    {
        randomGeneratorMock = Substitute.For<IRandomGenerator>();
        testedShipGenerator = new RandomShipGenerator(randomGeneratorMock);
    }
    
    [Test]
    public void CanGenerateHorizontalShipTest()
    {
        // Arrange
        const int maxFirstCellIndex = 10;
        const int expectedShipLenght = 3;
        var expectedShipStartCell = new Cell(1, 1);
        var expectedShipCells = new List<Cell>
        {
            expectedShipStartCell, new Cell(1, 2), new Cell(1, 3)
        };
            
        MockShipStartCell(expectedShipStartCell);
        MockShipOrientations(ShipOrientation.Horizontal);
        // Act

        var actualShip = testedShipGenerator.GetRandomShip(maxFirstCellIndex, expectedShipLenght);

        // Assert
        ExpectMaxIndexPassedCorrectly(maxFirstCellIndex);
        AssertShip(expectedShipCells, actualShip);
    }

    [Test]
    public void CanGenerateVerticalShipTest()
    {
        // Arrange
        const int maxFirstCellIndex = 10;
        const int expectedShipLenght = 3;
        var expectedShipStartCell = new Cell(RowIndex: 0, ColumnIndex: 0);
        var expectedShipCells = new List<Cell>
        {
            expectedShipStartCell, new Cell(1, 0), new Cell(2, 0)
        };
        
        MockShipStartCell(expectedShipStartCell);
        MockShipOrientations(ShipOrientation.Vertical);

        // Act
        var actualShip = testedShipGenerator.GetRandomShip(maxFirstCellIndex, expectedShipLenght);

        // Assert
        ExpectMaxIndexPassedCorrectly(maxFirstCellIndex);
        AssertShip(expectedShipCells, actualShip);
    }

    private void MockShipOrientations(ShipOrientation shipOrientation)
    {
        randomGeneratorMock
            .GetRandomOrientation()
            .Returns(shipOrientation);
    }

    private void MockShipStartCell(Cell cell)
    {
        randomGeneratorMock
            .GetRandomCell(Arg.Any<int>())
            .Returns(cell);
    }

    private void ExpectMaxIndexPassedCorrectly(int expectedMaxFirstCellIndex)
    {
        randomGeneratorMock
            .Received()
            .GetRandomCell(expectedMaxFirstCellIndex);
    }
    
    private static void AssertShip(List<Cell> expectedShipCells, Ship actualShip)
    {
        Assert.That(actualShip, Is.Not.Null);
        Assert.That(actualShip.Alive, Is.True);

        CollectionAssert.AreEqual(expectedShipCells, actualShip.Cells);
    }
}
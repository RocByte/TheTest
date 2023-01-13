using BattleshipsGame.Common;
using BattleshipsGame.Computer;
using BattleshipsGame.Computer.Contract;
using BattleshipsGame.Core;
using BattleshipsGame.Core.Contract;

namespace BattleshipsGameTests.Computer;

public class ComputerPlayerTest
{
    private const int SquareGridSize = 3;
    
    private ComputerPlayer testedPlayer = null!;
    private IRandomShipGenerator shipGeneratorMock = null!;
    private ShipRegister actualShipRegister = null!;
    
    [SetUp]
    public void SetUp()
    {
        actualShipRegister = new ShipRegister();
        shipGeneratorMock = Substitute.For<IRandomShipGenerator>();
    }

    [Test]
    public void CanPlaceSingleShipTest()
    {
        // Arrange
        CreateTestPlayer(initialShipSizes: new List<int> { 2 });
        
        var initialShip = GetShip(new Cell(1, 1), new Cell(1, 2));
        
        MockShips(initialShip);
            
        // Act
        var actualShips = ExecutePlaceShipsTest();

        // Assert
        ExpectShipGeneration(expectedShipLenght: 2, triesCount: 1);
        
        Assert.That(actualShips, Has.Count.EqualTo(1));
        Assert.That(actualShips[0], Is.Not.Null);
        Assert.That(actualShips[0], Is.EqualTo(initialShip));
    }
    
    [Test]
    public void CanPlaceShipWithDifferentSizesTest()
    {
        // Arrange
        CreateTestPlayer(initialShipSizes: new List<int> { 2, 1 });
        
        var initialShip1 = GetShip(new Cell(1, 1), new Cell(1, 2));
        var initialShip2 = GetShip(new Cell(2, 2));
        
        MockShips(initialShip1, initialShip2);
        
        // Act
        var actualShips = ExecutePlaceShipsTest();

        // Assert
        ExpectShipGeneration(expectedShipLenght: 2, triesCount: 1);
        ExpectShipGeneration(expectedShipLenght: 1, triesCount: 1);
        
        Assert.That(actualShips, Has.Count.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(actualShips[0], Is.EqualTo(initialShip1));
            Assert.That(actualShips[1], Is.EqualTo(initialShip2));
        });
    }

    [Test]
    public void CanNotPlaceShipsThatDoNotFitGridTest()
    {
        // Arrange
        CreateTestPlayer(initialShipSizes: new List<int> { 2 });

        const int toSmallIndex = -1;
        const int toBigIndex = 3;
        
        MockShips(
            GetShip(new Cell(toSmallIndex, 0), new Cell(0, 0)),
            GetShip(new Cell(0, toSmallIndex), new Cell(0, 0)),
            GetShip(new Cell(2, 2), new Cell(2, toBigIndex)),
            GetShip(new Cell(2, 2), new Cell(toBigIndex, 2)),
            GetShip(new Cell(0, 0), new Cell(1, 1)));
        
        // Act
        var actualShips = ExecutePlaceShipsTest();
        
        // Assert
        ExpectShipGeneration(expectedShipLenght: 2, triesCount: 5);
        
        Assert.That(actualShips, Has.Count.EqualTo(1));
    }
    
    [Test]
    public void CanNotPlaceShipsThatOverlapTest()
    {
        // Arrange

        CreateTestPlayer(initialShipSizes: new List<int> { 2, 2 });

        var ship1 = GetShip(new Cell(1, 1), new Cell(1, 2));
        
        MockShips(
            ship1,
            GetShip( new Cell(0, 1), ship1.Cells[0]),
            GetShip(ship1.Cells[1], new Cell(2, 2)),
            GetShip(new Cell(0, 0), new Cell(0, 1)));
        
        // Act
        var actualShips = ExecutePlaceShipsTest();
        
        // Assert
        ExpectShipGeneration(expectedShipLenght: 2, triesCount: 4);
        
        Assert.That(actualShips, Has.Count.EqualTo(2));
    }
        
    [Test]
    public void MaxShipGenerationAttemptsExceededTest()
    {
        // Arrange
        CreateTestPlayer(
            initialShipSizes: new List<int> { 1 },
            maxGenerationAttempts: 2);

        MockShips(
            GetShip(new Cell(100, 0)),
            GetShip(new Cell(100, 0)));
        
        // Act Assert
        Assert.Throws<ArgumentOutOfRangeException>(
            () => ExecutePlaceShipsTest());
    }
    
    private void MockShips(params Ship[] ships)
    {
        shipGeneratorMock
            .GetRandomShip(Arg.Any<int>(), Arg.Any<int>())
            .Returns(ships[0], ships.Skip(1).ToArray());
    }

    private void ExpectShipGeneration(
        int expectedShipLenght,
        int triesCount)
    {
        const int expectedMaxFirstCellIndex = SquareGridSize - 1;

        shipGeneratorMock
            .Received(triesCount)
            .GetRandomShip(expectedMaxFirstCellIndex, expectedShipLenght);
    }

    private void CreateTestPlayer(
        IList<int> initialShipSizes,
        int maxGenerationAttempts = 10)
    {
        testedPlayer = new ComputerPlayer(
            shipGeneratorMock,
            new ShipPlacementRules(new SquareGridAreaValidator(SquareGridSize)),
            initialShipSizes,
            maxGenerationAttempts);
    }
    
    private IList<Ship> ExecutePlaceShipsTest()
    {
        testedPlayer.PlaceShips(actualShipRegister);
        return actualShipRegister.AllShips;
    }
    
    private static Ship GetShip(params Cell[] cells)
    {
        return new Ship(cells);
    }
}
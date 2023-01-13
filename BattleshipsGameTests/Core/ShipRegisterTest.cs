using BattleshipsGame.Core;
using BattleshipsGame.Core.Contract;

namespace BattleshipsGameTests.Core;

public class ShipRegisterTest
{
    private ShipRegister testedShipRegister = null!;

    [SetUp]
    public void Setup()
    {
        testedShipRegister = new ShipRegister();
    }

    [Test]
    public void EmptyRegisterTest()
    {
        Assert.That(testedShipRegister.AllShips, Is.Not.Null);
        Assert.That(testedShipRegister.AllShips, Is.Empty);
        Assert.That(testedShipRegister.AnyShipLeft, Is.False);
    }

    [Test]
    public void AddShipTest()
    {
        testedShipRegister.AddShip(ShipInRow(0));
        
        Assert.Multiple(() =>
        {
            Assert.That(testedShipRegister.AllShips, Has.Count.EqualTo(1));
            Assert.That(testedShipRegister.AnyShipLeft, Is.True);
        });
    }

    [Test]
    public void AddSecondShipTest()
    {
        testedShipRegister.AddShip(ShipInRow(0));
        testedShipRegister.AddShip(ShipInRow(1));
        
        Assert.Multiple(() =>
        {
            Assert.That(testedShipRegister.AllShips, Has.Count.EqualTo(2));
            Assert.That(testedShipRegister.AnyShipLeft, Is.True);
        });
    }

    [Test]
    public void GetExitingShipTest()
    {
        // Arrange
        var initialShip = ShipInRow(0);
        testedShipRegister.AddShip(initialShip);
        
        // Act
        var actualShip = testedShipRegister.GetShip(new Cell(0, 0));
        var actualShip2 = testedShipRegister.GetShip(new Cell(0, 1));
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(initialShip, Is.EqualTo(actualShip));
            Assert.That(initialShip, Is.EqualTo(actualShip2));
        });
    }
    
    [Test]
    public void GetNotExitingShipTest()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => testedShipRegister.GetShip(new Cell(0, 0)));
    }
    
    [Test]
    public void MissShipTest()
    {
        var actualResult = testedShipRegister.Shoot(new Cell(0, 0));
        
        Assert.That(actualResult, Is.EqualTo(ShotResult.Miss));
    }    
    
    [Test]
    public void HitShipTest()
    {
        // Arrange
        testedShipRegister.AddShip(ShipInRow(0));
        
        // Act
        var actualResult = testedShipRegister.Shoot(new Cell(0, 0));
        
        // Assert
        AssertShotResult(actualResult, ShotResult.Hit, expectedShipsCount: 1, expectedShipsLeft: true);
    }

    [Test]
    public void SunkSingleShipTest()
    {
        // Arrange
        testedShipRegister.AddShip(ShipInRow(0));
        testedShipRegister.Shoot(new Cell(0, 0));
        
        // Act
        var actualResult = testedShipRegister.Shoot(new Cell(0, 1));
        
        // Assert
        AssertShotResult(actualResult, ShotResult.Sunk, expectedShipsCount: 1, expectedShipsLeft: false);
    }
    
    [Test]
    public void SunkOneOfTwoShipsTest()
    {
        // Arrange
        testedShipRegister.AddShip(ShipInRow(0));
        testedShipRegister.AddShip(ShipInRow(1));
        testedShipRegister.Shoot(new Cell(0, 0));
        
        // Act
        var actualResult = testedShipRegister.Shoot(new Cell(0, 1));
        
        // Assert
        AssertShotResult(actualResult, ShotResult.Sunk, expectedShipsCount: 2, expectedShipsLeft: true);
    }
    
    [Test]
    public void SunkAllShipsTest()
    {
        // Arrange
        testedShipRegister.AddShip(ShipInRow(0));
        testedShipRegister.AddShip(ShipInRow(1));
        testedShipRegister.Shoot(new Cell(0, 0));
        testedShipRegister.Shoot(new Cell(0, 1));
        testedShipRegister.Shoot(new Cell(1, 0));
        
        // Act
        var actualResult = testedShipRegister.Shoot(new Cell(1, 1));
        
        // Assert
        AssertShotResult(actualResult, ShotResult.Sunk, expectedShipsCount: 2, expectedShipsLeft: false);
    }
    
    private void AssertShotResult(
        ShotResult actualResult, 
        ShotResult expectedShotResult,
        int expectedShipsCount,
        bool expectedShipsLeft)
    {
        Assert.Multiple(() =>
        {
            Assert.That(actualResult, Is.EqualTo(expectedShotResult));
            Assert.That(testedShipRegister.AllShips, Has.Count.EqualTo(expectedShipsCount));
            Assert.That(testedShipRegister.AnyShipLeft, Is.EqualTo(expectedShipsLeft));
        });
    }

    private static Ship ShipInRow(int rowIndex)
    {
        return new Ship(new[]
        {
            new Cell(rowIndex, 0),
            new Cell(rowIndex, 1)
        });
    }
}
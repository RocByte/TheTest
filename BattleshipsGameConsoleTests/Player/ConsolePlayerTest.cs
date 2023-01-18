using BattleshipsGame.Core.Contract;
using BattleshipsGameConsole.Player;
using BattleshipsGameConsole.Player.Contract;

namespace BattleshipsGameConsoleTests.Player;

public class ConsolePlayerTest
{
    private ConsolePlayer testedPlayer = null!;
    private IPlayerInputProvider inputProviderMock = null!;
    private IShipRegister shipRegisterMock = null!;
    private IPlayerOutputPresenter outputPresenterMock = null!;

    [SetUp]
    public void SetUp()
    {
        inputProviderMock = Substitute.For<IPlayerInputProvider>();
        shipRegisterMock = Substitute.For<IShipRegister>();
        outputPresenterMock = Substitute.For<IPlayerOutputPresenter>();

        testedPlayer = new ConsolePlayer(
            inputProviderMock,
            outputPresenterMock);
    }

    [Test]
    public void ValidShotTest()
    {
        // Arrange
        var validCell = new Cell(0, 0);
        var validShotResult = ShotResult.Hit;
        
        inputProviderMock
            .TryGetTargetCell()
            .Returns(validCell);
        
        shipRegisterMock
            .Shoot(validCell)
            .Returns(validShotResult);
        
        // Act
        var actualResult = testedPlayer.TryShoot(shipRegisterMock);

        // Assert
        Assert.That(actualResult, Is.True);
        ExpectShipRegisterAndViewUpdate(validCell, validShotResult);
    }

    [Test]
    public void InValidShotTest()
    {
        // Arrange
        inputProviderMock
            .TryGetTargetCell()
            .Returns((Cell?)null);
        
        // Act
        var actualResult = testedPlayer.TryShoot(shipRegisterMock);

        // Assert
        Assert.That(actualResult, Is.False);

        ExpectNoShipRegisterOrViewUpdate();
    }

    private void ExpectShipRegisterAndViewUpdate(Cell validCell, ShotResult validShotResult)
    {
        shipRegisterMock
            .Received()
            .Shoot(validCell);

        outputPresenterMock
            .Received()
            .ShowShotResult(validCell, validShotResult, shipRegisterMock);
    }
    
    private void ExpectNoShipRegisterOrViewUpdate()
    {
        shipRegisterMock
            .DidNotReceive()
            .Shoot(Arg.Any<Cell>());

        outputPresenterMock
            .DidNotReceive()
            .ShowShotResult(Arg.Any<Cell>(), Arg.Any<ShotResult>(), Arg.Any<IShipRegister>());
    }
}
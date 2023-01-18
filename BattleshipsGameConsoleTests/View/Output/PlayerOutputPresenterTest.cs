using BattleshipsGame.Core.Contract;
using BattleshipsGameConsole.View.Output;
using BattleshipsGameConsole.View.Output.Contract;

namespace BattleshipsGameConsoleTests.View.Output;

public class PlayerOutputPresenterTest
{
    private PlayerOutputPresenter testedOutputPresenter = null!;
    private IPlayerBoardUpdater boardUpdaterMock = null!;
    private IConsoleView consoleViewMock = null!;
    private IShipRegister shipRegisterMock = null!;
    
    private readonly PlayerBoard updaterBoard = new PlayerBoard(1);
    
    [SetUp]
    public void SetUp()
    {
        boardUpdaterMock = Substitute.For<IPlayerBoardUpdater>();
        consoleViewMock = Substitute.For<IConsoleView>();
        shipRegisterMock = Substitute.For<IShipRegister>();
        testedOutputPresenter = new PlayerOutputPresenter(boardUpdaterMock, consoleViewMock);
        
        boardUpdaterMock.Board
            .Returns(updaterBoard);
    }

    [Test]
    public void BoardInitializedAndShownOnStartTest()
    {
        var initialBoard = new PlayerBoard(1);

        // Arrange
        boardUpdaterMock
            .When(boardUpdater => boardUpdater.InitializeBoardState())
            .Do(_ => MockPlayerBoard(initialBoard));

        // Act;
        testedOutputPresenter.ShowInitializedBoard();;

        // Assert
        consoleViewMock
            .Received()
            .ShowBoardReadyToShot(initialBoard);
    }
    
    [TestCase(ShotResult.Miss)]
    [TestCase(ShotResult.Hit)]
    public void ApplyShotResultTest(ShotResult shotResult)
    {
        // Arrange
        var initialBoard = new PlayerBoard(1);
        MockPlayerBoard(initialBoard);
        
        var targetCell = new Cell(0, 0);

        // Act
        testedOutputPresenter.ShowShotResult(targetCell, shotResult, shipRegisterMock);
        
        // Assert
        ExpectCellStateSet(targetCell, shotResult);
        
        consoleViewMock
            .Received()
            .ShowBoardReadyToShot(initialBoard);
    }
    
    [Test]
    public void ApplySinkShootResultTest()
    {
        // Arrange
        var initialBoard = new PlayerBoard(1);
        MockPlayerBoard(initialBoard);
        
        var targetCell = new Cell(0, 0);
        var targetShipOtherCell = new Cell(0, 1);
        var initialShip = GetShip(targetCell, targetShipOtherCell);

        shipRegisterMock
            .GetShip(targetCell)
            .Returns(initialShip);
        
        // Act
        testedOutputPresenter.ShowShotResult(targetCell, ShotResult.Sunk, shipRegisterMock);
        
        // Assert
        ExpectCellStateSet(targetCell, ShotResult.Sunk);
        ExpectCellStateSet(targetShipOtherCell, ShotResult.Sunk);
        
        consoleViewMock
            .Received()
            .ShowBoardReadyToShot(initialBoard);
    }
    
    [Test]
    public void ShowAllShipsAsSunkOnGameOverTest()
    {
        // Arrange
        var initialBoard = new PlayerBoard(1);
        MockPlayerBoard(initialBoard);
        
        var initialShips = new[]
        {
            GetShip(new Cell(0, 0), new Cell(0, 1)),
            GetShip(new Cell(1, 0), new Cell(1, 1))
        };
        
        // Act
        testedOutputPresenter.ShowGameOver(initialShips);
        
        // Assert
        ExpectShipsSunk(initialShips);
        
        consoleViewMock
            .Received()
            .ShowGameOver(initialBoard);
    }
    
    private void MockPlayerBoard(PlayerBoard initialBoard)
    {
        boardUpdaterMock.Board.Returns(initialBoard);
    }
    
    private void ExpectShipsSunk(IEnumerable<Ship> ships)
    {
        foreach (var ship in ships)
        {
            foreach (var expectedCell in ship.Cells)
            {
                ExpectCellStateSet(expectedCell, ShotResult.Sunk);
            }
        }
    }

    private void ExpectCellStateSet(Cell expectedCell, ShotResult expectedState)
    {
        boardUpdaterMock
            .Received()
            .SetState(expectedCell, expectedState);
    }

    private static Ship GetShip(params Cell[] cells)
    {
        return new Ship(cells);
    }
}
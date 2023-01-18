using BattleshipsGame.Core.Contract;
using BattleshipsGameConsole.Player.Contract;
using BattleshipsGameConsole.View.Output.Contract;

namespace BattleshipsGameConsole.View.Output;

public class PlayerOutputPresenter : IPlayerOutputPresenter
{
    private readonly IPlayerBoardUpdater playerBoardUpdater;
    private readonly IConsoleView consoleView;

    public PlayerOutputPresenter(
        IPlayerBoardUpdater playerBoardUpdater,
        IConsoleView consoleView)
    {
        this.playerBoardUpdater = playerBoardUpdater;
        this.consoleView = consoleView;
    }

    public void ShowInitializedBoard()
    {
        playerBoardUpdater.InitializeBoardState();
        consoleView.ShowBoardReadyToShot(playerBoardUpdater.Board);
    }
    
    public void ShowShotResult(
        Cell targetCell, 
        ShotResult shotResult,
        IShipRegister shipRegister)
    {
        if (shotResult == ShotResult.Sunk)
        {
            var sunkShip = shipRegister.GetShip(targetCell);
            SinkShip(sunkShip);
        }
        else
        {
            ShotCell(targetCell, shotResult);
        }
        
        consoleView.ShowBoardReadyToShot(playerBoardUpdater.Board);
    }

    public void ShowGameOver(IList<Ship> shipsToDisplay)
    {
        foreach (var ship in shipsToDisplay)
        {
            SinkShip(ship);
        }
        
        consoleView.ShowGameOver(playerBoardUpdater.Board);
    }

    public void ShowWin()
    {
        consoleView.ShowWin();
    }

    private void SinkShip(Ship ship)
    {
        foreach (var cell in ship.Cells)
        {
            ShotCell(cell, ShotResult.Sunk);
        }
    }

    private void ShotCell(Cell targetCell, ShotResult shotResult)
    {
        playerBoardUpdater.SetState(targetCell, shotResult);
    }
}
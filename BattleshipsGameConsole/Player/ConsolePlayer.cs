using BattleshipsGame.Core.Contract;
using BattleshipsGameConsole.Player.Contract;

namespace BattleshipsGameConsole.Player;

public class ConsolePlayer : IHumanPlayer
{
    private readonly IPlayerOutputPresenter playerOutputPresenter;
    private readonly IPlayerInputProvider playerInputProvider;

    public ConsolePlayer(
        IPlayerInputProvider playerInputProvider,
        IPlayerOutputPresenter playerOutputPresenter)
    {
        this.playerInputProvider = playerInputProvider;
        this.playerOutputPresenter = playerOutputPresenter;
    }

    public void ShowStart()
    {
        playerOutputPresenter.ShowInitializedBoard();
    }

    public bool TryShoot(IShipRegister shipRegister)
    {
        var targetCell = playerInputProvider.TryGetTargetCell();
        if (targetCell == null)
            return false;

        var shotResult = shipRegister.Shoot(targetCell);
        
        playerOutputPresenter.ShowShotResult(
            targetCell,
            shotResult,
            shipRegister);
        
        return true;
    }
    
    public void ShowWin()
    {
        playerOutputPresenter.ShowWin();
    }

    public void ShowGameOver(IShipRegister shipRegister)
    {
        playerOutputPresenter.ShowGameOver(shipRegister.AllShips);
    }
}
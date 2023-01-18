using BattleshipsGame.Core.Contract;

namespace BattleshipsGameConsole.Player.Contract;

public interface IPlayerOutputPresenter
{
    void ShowInitializedBoard();
    
    void ShowShotResult(
        Cell targetCell, 
        ShotResult shotResult,
        IShipRegister shipRegister);

    void ShowGameOver(IList<Ship> shipsToDisplay);

    void ShowWin();
}
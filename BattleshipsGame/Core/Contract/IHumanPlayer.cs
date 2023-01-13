namespace BattleshipsGame.Core.Contract;

public interface IHumanPlayer
{
    void ShowStart();
    void ShowWin();
    bool TryShoot(IShipRegister shipRegister);
    void ShowGameOver(IShipRegister shipRegister);
}
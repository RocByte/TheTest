namespace BattleshipsGameConsole.View.Output.Contract;

public interface IConsoleView
{
    public void ShowBoardReadyToShot(PlayerBoard playerBoard);
    void ShowWin();
    void ShowGameOver(PlayerBoard playerBoard);
    public void ShowInvalidShot(string inputLine);
    public void ShowOutOfRangeShot(string inputLine);
}
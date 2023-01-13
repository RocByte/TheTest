using BattleshipsGame.Computer;
using BattleshipsGame.Core.Contract;

namespace BattleshipsGame.Core;

public class TargetPracticeMatch
{
    private readonly ComputerPlayer computerPlayer;
    private readonly IHumanPlayer humanPlayer;
    private readonly ShipRegister shipRegister;

    public TargetPracticeMatch(
        ComputerPlayer computerPlayer, 
        IHumanPlayer humanPlayer)
    {
        this.computerPlayer = computerPlayer;
        this.humanPlayer = humanPlayer;
        shipRegister = new ShipRegister();
    }

    public void Start()
    {
        computerPlayer.PlaceShips(shipRegister);
        humanPlayer.ShowStart();
        
        do
        {
            var shotMade = humanPlayer.TryShoot(shipRegister);
            if (shotMade == false)
                break;
        } while (shipRegister.AnyShipLeft());
        
        ShowMatchResult();
    }

    private void ShowMatchResult()
    {
        if (shipRegister.AnyShipLeft())
            humanPlayer.ShowGameOver(shipRegister);
        else
            humanPlayer.ShowWin();
    }
}

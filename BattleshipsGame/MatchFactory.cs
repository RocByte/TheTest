using BattleshipsGame.Common;
using BattleshipsGame.Computer;
using BattleshipsGame.Core;
using BattleshipsGame.Core.Contract;

namespace BattleshipsGame;

public static class MatchFactory
{
    public static TargetPracticeMatch GetTargetPracticeMatch(
        IHumanPlayer humanPlayer,
        int squareGridSize,
        IList<int> initialShipSizes)
    {
        var computerPlayer = new ComputerPlayer(
            new RandomShipGenerator(new RandomGenerator()),
            new ShipPlacementRules(new SquareGridAreaValidator(squareGridSize)),
            initialShipSizes,
            maxShipGenerationAttempts: 1000);

        return new TargetPracticeMatch(
            computerPlayer,
            humanPlayer);
    }
}
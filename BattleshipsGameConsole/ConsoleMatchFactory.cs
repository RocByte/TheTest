using BattleshipsGame;
using BattleshipsGame.Common;
using BattleshipsGame.Core;
using BattleshipsGameConsole.Player;
using BattleshipsGameConsole.View.Input;
using BattleshipsGameConsole.View.Input.Contract;
using BattleshipsGameConsole.View.Output;
using BattleshipsGameConsole.View.Output.Contract;

namespace BattleshipsGameConsole;

public static class ConsoleMatchFactory
{
    public static TargetPracticeMatch GetTargetPracticeMatch(
        int squareGridSize,
        IList<int> initialShipSizes)
    {
        return GetTargetPracticeMatch(
            squareGridSize,
            initialShipSizes,
            new ConsoleView(),
            new ConsoleReader());
    }
    
    public static TargetPracticeMatch GetTargetPracticeMatch(
        int squareGridSize,
        IList<int> initialShipSizes,
        IConsoleView consoleView,
        IConsoleReader consoleReader)
    {
        var consolePlayer = CreateConsolePlayer(
            squareGridSize, 
            consoleView,
            consoleReader);

        return MatchFactory.GetTargetPracticeMatch(
            consolePlayer,
            squareGridSize,
            initialShipSizes);
    }

    private static ConsolePlayer CreateConsolePlayer(
        int squareGridSize, 
        IConsoleView consoleView,
        IConsoleReader consoleReader)
    {
        var playerInputProvider = new PlayerInputProvider(
            consoleReader, 
            new ConsoleInputParser(), 
            new SquareGridAreaValidator(squareGridSize),
            consoleView);

        var playerOutputPresenter = new PlayerOutputPresenter(
            new PlayerBoardUpdater(squareGridSize),
            consoleView);

        return new ConsolePlayer(
            playerInputProvider,
            playerOutputPresenter);
    }
}
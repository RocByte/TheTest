using BattleshipsGame.Common;
using BattleshipsGame.Core.Contract;
using BattleshipsGameConsole.Player.Contract;
using BattleshipsGameConsole.View.Input.Contract;
using BattleshipsGameConsole.View.Output.Contract;

namespace BattleshipsGameConsole.View.Input;

public class PlayerInputProvider : IPlayerInputProvider
{
    private static readonly string[] ExitStrings = InputParameters.ExitStrings;
    
    private readonly IConsoleReader consoleReader;
    private readonly IConsoleInputParser consoleInputParser;
    private readonly SquareGridAreaValidator shotValidator;
    private readonly IConsoleView consoleView;

    public PlayerInputProvider(
        IConsoleReader consoleReader,
        IConsoleInputParser consoleInputParser,
        SquareGridAreaValidator shotValidator,
        IConsoleView consoleView)
    {
        this.consoleReader = consoleReader;
        this.consoleInputParser = consoleInputParser;
        this.shotValidator = shotValidator;
        this.consoleView = consoleView;
    }

    public Cell? TryGetTargetCell()
    {
        while (true)
        {
            var line = TryGetLine();
            if (line == null)
                return null;

            var targetCell = consoleInputParser.TryParse(line);
            if (targetCell == null)
            {
                consoleView.ShowInvalidShot(line);
                continue;
            }
            
            if (shotValidator.FitsInSquareGrid(targetCell))
                return targetCell;
            
            consoleView.ShowOutOfRangeShot(line);
        }
    }
    
    private string? TryGetLine()
    {
        var line = consoleReader.ReadLine();
        if (line == null)
            return null;

        return ExitStrings.Contains(line.ToLower())
            ? null
            : line;
    }
}
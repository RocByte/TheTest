using BattleshipsGame.Core.Contract;

namespace BattleshipsGameConsole.View.Input.Contract;

public interface IConsoleInputParser
{
    Cell? TryParse(string coordinatesString);
}
using BattleshipsGameConsole.View.Input.Contract;

namespace BattleshipsGameConsole.View.Input;

public class ConsoleReader : IConsoleReader
{
    public string? ReadLine()
    {
        return Console.ReadLine();
    }
}
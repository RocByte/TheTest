using BattleshipsGameConsole.View.Output.Contract;

namespace BattleshipsGameConsole.View.Output;

public class ConsoleView : IConsoleView
{
    public void ShowBoardReadyToShot(PlayerBoard playerBoard)
    {
        RedrawPlayerBoard(playerBoard);
        Console.WriteLine("Type shot coordinates like 'B10' + 'Enter' or type 'esc' or 'exit' + 'Enter' to exit");
    }

    public void ShowWin()
    {
        Console.WriteLine("Amazing. You win. Now do something with your live.");
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    public void ShowGameOver(PlayerBoard playerBoard)
    {
        RedrawPlayerBoard(playerBoard);
        Console.WriteLine("And here they are.");
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    public void ShowInvalidShot(string inputLine)
    {
        Console.WriteLine($"No idea what should I do with '{inputLine}'. Try again!");
    }

    public void ShowOutOfRangeShot(string inputLine)
    {
        Console.WriteLine($"Shot '{inputLine}' is out of range. Try again!");
    }

    public static void DrawPlayerBoard(PlayerBoard playerBoard)
    {
        for (var rowIndex = 0; rowIndex < playerBoard.Size; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < playerBoard.Size; columnIndex++)
            {
                var cellValue = playerBoard.GridValues[rowIndex, columnIndex];
                cellValue ??= " ";
                Console.Write(cellValue);
            }
            Console.WriteLine();
        }
    }
    
    private static void RedrawPlayerBoard(PlayerBoard playerBoard)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();
        DrawPlayerBoard(playerBoard);
    }
}
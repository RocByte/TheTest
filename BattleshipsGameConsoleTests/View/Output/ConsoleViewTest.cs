using BattleshipsGameConsole.View.Output;

namespace BattleshipsGameConsoleTests.View.Output;

public class ConsoleViewTest
{
    StringWriter consoleStringWriter = null!;

    [SetUp]
    public void SetUp()
    {
        consoleStringWriter =  new StringWriter();
        Console.SetOut(consoleStringWriter);
        Console.SetError(consoleStringWriter);
    }
    
    [Test]
    public void NullCellValuesShownAsSpacesTest()
    {
        // Arrange
        var initialBoard = new PlayerBoard(2);
        initialBoard.GridValues[0, 0] = "Cell00";
        initialBoard.GridValues[1, 1] = "Cell11";

        // Act
        ConsoleView.DrawPlayerBoard(initialBoard);
        var actualConsoleText = consoleStringWriter.ToString();

        // Assert
        var expectedConsoleText = $"Cell00 {Environment.NewLine} Cell11{Environment.NewLine}";
        Assert.That(actualConsoleText, Is.EqualTo(expectedConsoleText));
    }
}
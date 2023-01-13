using BattleshipsGame.Core.Contract;
using BattleshipsGameConsole.View.Output;

namespace BattleshipsGameConsoleTests.View.Output;

public class PlayerBoardUpdaterTest
{
    private PlayerBoardUpdater testedUpdater = null!;
    private PlayerBoard actualBoard = null!;

    [Test]
    public void BoardInitialStateTest()
    {
        // Arrange
        CreateTestedController(valueGridSize: 3);
        
        // Act
        testedUpdater.InitializeBoardState();
        
        // Assert
        ConsoleView.DrawPlayerBoard(actualBoard);
        
        Assert.Multiple(() =>
        {
            Assert.That(actualBoard.Size, Is.EqualTo(8));
            Assert.That(actualBoard.GridValues, Is.EqualTo(GetExpectedGridValues()));
        });
    }

    private static string?[,] GetExpectedGridValues()
    {
        return new[,] {
            {"#", "|" , "A", "|", "B", "|", "C", "|"},
            {"-", "-" , "-", "-", "-", "-", "-", "-"},
            {"1", "|" , null, "|", null, "|", null, "|"},
            {"-", "-" , "-", "-", "-", "-", "-", "-"},
            {"2", "|" , null, "|", null, "|", null, "|"},
            {"-", "-" , "-", "-", "-", "-", "-", "-"},
            {"3", "|" , null, "|", null, "|", null, "|"},
            {"-", "-" , "-", "-", "-", "-", "-", "-"}
        };
    }
    
    [Test]
    public void EachShotResultHasDifferentStateOnBoardTest()
    {
        // Arrange
        CreateTestedController(valueGridSize: 3);
        testedUpdater.InitializeBoardState();
        
        // Act
        testedUpdater.SetState(new Cell(0, 0), ShotResult.Miss);
        testedUpdater.SetState(new Cell(1, 1), ShotResult.Hit);
        testedUpdater.SetState(new Cell(2, 2), ShotResult.Sunk);
        
        // Assert
        ConsoleView.DrawPlayerBoard(actualBoard);
        
        var expectedGridValues = GetExpectedGridValues();
        expectedGridValues[2, 2] = ViewParameters.ShotMiss;
        expectedGridValues[4, 4] = ViewParameters.ShotHit;
        expectedGridValues[6, 6] = ViewParameters.ShotSunk;
        
        Assert.That(actualBoard.GridValues, Is.EqualTo(expectedGridValues));
    }

    [Test]
    public void RowAndColumnHeadersShownCorrectInBiggerBoardTest()
    {
        // Arrange
        CreateTestedController(valueGridSize: 10);

        // Act
        testedUpdater.InitializeBoardState();

        // Assert
        ConsoleView.DrawPlayerBoard(actualBoard);

        Assert.That(actualBoard.Size, Is.EqualTo(22));
        
        Assert.Multiple(() =>
        {
            var expectedTopLefCell = new string(ViewParameters.TopLeftCellFillChar, 2);
            Assert.That(actualBoard.GridValues[0, 0], Is.EqualTo(expectedTopLefCell));

            var expectedRowSeparator = new string(ViewParameters.RowSeparatorChar, 2);
            Assert.That(actualBoard.GridValues[1, 0], Is.EqualTo(expectedRowSeparator));
            Assert.That(actualBoard.GridValues[2, 0], Is.EqualTo(" 1"));
            Assert.That(actualBoard.GridValues[20, 0], Is.EqualTo("10"));
            Assert.That(actualBoard.GridValues[21, 0], Is.EqualTo(expectedRowSeparator));

            Assert.That(actualBoard.GridValues[0, 20], Is.EqualTo("J"));
            Assert.That(actualBoard.GridValues[0, 21], Is.EqualTo(ViewParameters.ColumnSeparator));
        });
    }

    private void CreateTestedController(int valueGridSize)
    {
        testedUpdater = new PlayerBoardUpdater(valueGridSize);
        actualBoard = testedUpdater.Board;
    }
}
using BattleshipsGame.Common;
using BattleshipsGame.Core.Contract;
using BattleshipsGameConsole.View.Input;
using BattleshipsGameConsole.View.Input.Contract;
using BattleshipsGameConsole.View.Output.Contract;

namespace BattleshipsGameConsoleTests.View.Input;

public class PlayerInputProviderTest
{
    private const int SquareGridSize = 3;
    private const string? ExitAppUserInputValue = null;
    
    private static readonly Cell ValidCell = new Cell(SquareGridSize - 1,SquareGridSize - 1);
    private static readonly Cell OutOfRangeCell = new Cell(SquareGridSize,SquareGridSize);
    
    private PlayerInputProvider testedInputProvider = null!;
    private IConsoleReader consoleReaderMock = null!;
    private IConsoleInputParser inputParserMock = null!;
    private IConsoleView consoleViewMock = null!;
    
    [SetUp]
    public void SetUp()
    {
        consoleReaderMock = Substitute.For<IConsoleReader>();
        inputParserMock = Substitute.For<IConsoleInputParser>();
        consoleViewMock = Substitute.For<IConsoleView>();
        
        testedInputProvider = new PlayerInputProvider(
            consoleReaderMock,
            inputParserMock,
            new SquareGridAreaValidator(SquareGridSize),
            consoleViewMock);
    }

    [TestCase("validShotInput")]
    [TestCase("")]
    public void ValidUserInputIsParsedToCellTest(string inputLine)
    {
        // Arrange
        MockUserInputStrings(inputLine);
        MockInputToCellParsing(inputLine, ValidCell);
        
        // Act
        var actualResult = testedInputProvider.TryGetTargetCell();

        // Assert
        Assert.That(actualResult, Is.EqualTo(ValidCell));
    }
    
    [TestCase("ESC")]
    [TestCase("EsC")]
    [TestCase(InputParameters.ExitString1)]
    [TestCase(InputParameters.ExitString2)]
    [TestCase(null)]
    public void NullInputOrExitStringIsNotParsedToCell(string? inputLine)
    {
        // Arrange
        MockUserInputStrings(inputLine);

        // Act
        var actualResult = testedInputProvider.TryGetTargetCell();

        // Assert
        Assert.That(actualResult, Is.Null);
    }

    [Test]
    public void InvalidInputShownInConsoleAndNewOneIsRequestedTest()
    {
        // Arrange
        const string invalidShotInput = "invalidShot";
        MockUserInputStrings(invalidShotInput, ExitAppUserInputValue);

        // Act
        var actualResult = testedInputProvider.TryGetTargetCell();

        // Assert
        Assert.That(actualResult, Is.Null);
        
        consoleViewMock
            .Received()
            .ShowInvalidShot(invalidShotInput);
    }
    
    [Test]
    public void OutOfRangeShotShownInConsoleAndNewOneIsRequestedTest()
    {
        // Arrange
        const string outOfRangeShotInput = "outOfRangeShot";
        MockUserInputStrings(outOfRangeShotInput, ExitAppUserInputValue);
        MockInputToCellParsing(outOfRangeShotInput, OutOfRangeCell);
        
        // Act
        var actualResult = testedInputProvider.TryGetTargetCell();

        // Assert
        Assert.That(actualResult, Is.Null);
        
        consoleViewMock
            .Received()
            .ShowOutOfRangeShot(outOfRangeShotInput);
    }
    
    private void MockUserInputStrings(params string?[] userInputStrings)
    {
        consoleReaderMock
            .ReadLine()
            .Returns(userInputStrings[0], userInputStrings.Skip(1).ToArray());
    }
    
    private void MockInputToCellParsing(string userInput, Cell? parsedCell)
    {
        inputParserMock
            .TryParse(userInput)
            .Returns(parsedCell);
    }
}
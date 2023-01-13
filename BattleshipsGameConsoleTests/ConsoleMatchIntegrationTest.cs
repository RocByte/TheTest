using BattleshipsGame.Core;
using BattleshipsGameConsole;
using BattleshipsGameConsole.View.Input.Contract;
using BattleshipsGameConsole.View.Output;
using BattleshipsGameConsole.View.Output.Contract;

namespace BattleshipsGameConsoleTests;

public class ConsoleMatchIntegrationTest
{
    private TargetPracticeMatch testedMatch = null!;
    private IConsoleView consoleViewMock = null!;
    private IConsoleReader consoleReaderMock = null!;

    [SetUp]
    public void SetUp()
    {
        consoleViewMock = Substitute.For<IConsoleView>();
        consoleReaderMock = Substitute.For<IConsoleReader>();
        
        testedMatch = ConsoleMatchFactory.GetTargetPracticeMatch(
            squareGridSize: 2,
            initialShipSizes: new List<int> { 2 },
            consoleViewMock,
            consoleReaderMock);
    }
    
    [Test]
    public void CanStartAndExitMatchTest()
    {
        MockOneShotAndExit("A1");
        
        testedMatch.Start();
        
        ExpectShotResultSendToView(expectColumnIndex: 2, expectRowIndex: 2);
        
        consoleViewMock
            .Received()
            .ShowGameOver(Arg.Any<PlayerBoard>());
    }
    
    [Test]
    public void CanStartAndWinMatchTest()
    {
        var allPossibleShots = new[] { "A1", "A2", "B1", "B2" };
        MockShots(allPossibleShots);
        
        testedMatch.Start();
        
        consoleViewMock
            .Received()
            .ShowWin();
    }

    private void MockOneShotAndExit(string shotString)
    {
        string? exitCommandString = null;
        consoleReaderMock.ReadLine()
            .Returns(
                shotString,
                exitCommandString);
    }
    
    private void MockShots(params string[] shots)
    {
        consoleReaderMock.ReadLine()
            .Returns(shots[0], shots.Skip(1).ToArray());
    }

    private void ExpectShotResultSendToView(int expectColumnIndex, int expectRowIndex)
    {
        var possibleShotResults = new List<string?>
        {
            ViewParameters.ShotMiss,
            ViewParameters.ShotHit, 
            ViewParameters.ShotSunk
        };

        consoleViewMock
            .Received()
            .ShowBoardReadyToShot(Arg.Is<PlayerBoard>(
                board => possibleShotResults.Contains(
                    board.GridValues[expectRowIndex, expectColumnIndex])));
    }
}
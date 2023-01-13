using BattleshipsGameConsole.View.Input;

namespace BattleshipsGameConsoleTests.View.Input;

public class ConsoleInputParserTest
{
    private ConsoleInputParser testedParser= null!;
    
    [SetUp]
    public void SetUp()
    {
        testedParser = new ConsoleInputParser();
    }

    [TestCase("a0", 0, -1)]
    [TestCase("a1", 0, 0)]
    [TestCase("A1", 0, 0)]
    [TestCase(" A1 ", 0, 0)]
    [TestCase(" A 1 ", 0, 0)]
    [TestCase("A01", 0, 0)]
    [TestCase("A0001", 0, 0)]
    [TestCase("A010", 0, 9)]
    [TestCase("J10", 9, 9)]
    [TestCase("j10", 9, 9)]
    [TestCase(" j 10 ", 9, 9)]
    [TestCase("Z99", 25, 98, Description = "Max values")]
    public void ValidUserInputTest(
        string inputString, 
        int expectedColumnIndex,
        int expectedRowIndex)
    {
        var actualResult = testedParser.TryParse(inputString);
        
        Assert.That(actualResult, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(actualResult!.ColumnIndex, Is.EqualTo(expectedColumnIndex));
            Assert.That(actualResult.RowIndex, Is.EqualTo(expectedRowIndex));
        });
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    [TestCase("A")]
    [TestCase("1")]
    [TestCase("A-1")]
    [TestCase("1A")]
    [TestCase("AA")]
    [TestCase("A1A")]
    [TestCase("j 1 0")]
    [TestCase("A100", Description = "Max row number exceeded")]
    [TestCase("{1")]
    [TestCase("Š1", Description = "Max letter code exceeded")]
    public void InvalidUserInputTest(string inputString)
    {
        var actualResult = testedParser.TryParse(inputString);
        Assert.That(actualResult, Is.Null);
    }
}
using BattleshipsGame.Core.Contract;
using BattleshipsGameConsole.View.Input.Contract;
using BattleshipsGameConsole.View.Output;

namespace BattleshipsGameConsole.View.Input;

public class ConsoleInputParser : IConsoleInputParser
{
    private const int FirstLetterCode = ViewParameters.FirstColumnLetter;
    private const int LastLetterCode = ViewParameters.MaxLetterCode;
    private const int MaxRowNumber = ViewParameters.MaxRowNumber;

    public Cell? TryParse(string coordinatesString)
    {
        if (string.IsNullOrEmpty(coordinatesString))
            return null;

        coordinatesString = coordinatesString.Trim();
        
        if (coordinatesString.Length == 0)
            return null;

        var columnIndex = TryParseColumnIndex(coordinatesString[0]);
        if (columnIndex == null)
            return null;

        var rowString = coordinatesString.Substring(1);
        var rowIndex = TryParseRowIndex(rowString);
        if (rowIndex == null)
            return null;

        return new Cell(rowIndex.Value, columnIndex.Value);
    }

    private int? TryParseColumnIndex(char columnLetter)
    {
        var letterCode = (int)char.ToLower(columnLetter);
        if (letterCode > LastLetterCode)
            return null;
        
        var alphabetIndex = letterCode - FirstLetterCode;

        return alphabetIndex >= 0 
            ? alphabetIndex 
            : null;
    }
    
    private int? TryParseRowIndex(string rowString)
    {
        if (int.TryParse(rowString, out var rowNumber) == false)
            return null;

        if (rowNumber > MaxRowNumber)
            return null;
        
        return rowNumber >= 0 
            ?  rowNumber - 1
            : null;
    }
}
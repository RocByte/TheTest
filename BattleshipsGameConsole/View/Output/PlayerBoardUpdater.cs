using BattleshipsGame.Core.Contract;
using BattleshipsGameConsole.View.Output.Contract;

namespace BattleshipsGameConsole.View.Output;

public class PlayerBoardUpdater : IPlayerBoardUpdater
{
    private const char TopLeftCellFillChar = ViewParameters.TopLeftCellFillChar;
    private const char FirstColumnLetter = ViewParameters.FirstColumnLetter;
    private const int FirstRowNumber = ViewParameters.FirstRowNumber;
    private const char RowSeparatorChar = ViewParameters.RowSeparatorChar;
    private const string ColumnSeparator = ViewParameters.ColumnSeparator;
    private const string ShotMiss = ViewParameters.ShotMiss;
    private const string ShotHit = ViewParameters.ShotHit;
    private const string ShotSunk = ViewParameters.ShotSunk;
    private static readonly string RowSeparator = ViewParameters.RowSeparator;

    private const int HeaderColumnCount = 2;
    private const char PaddingChar = ' ';
    
    private readonly int rowColumnPaddingLength;
    private readonly string topLeftCellValue;
    private readonly string firstColumnRowSeparator;

    public PlayerBoard Board { get; }

    public PlayerBoardUpdater(int valueGridSize)
    {
        var boardSize = valueGridSize * 2 + HeaderColumnCount;
        Board = new PlayerBoard(boardSize);
        
        rowColumnPaddingLength = valueGridSize.ToString().Length;
        topLeftCellValue = new string(TopLeftCellFillChar, rowColumnPaddingLength);
        firstColumnRowSeparator = new string(RowSeparatorChar, rowColumnPaddingLength); 
    }
    
    public void InitializeBoardState()
    {
        AddTopLeftHeader(Board.GridValues);
        AddRowNumbers(Board.GridValues);
        SetColumnLetters(Board.GridValues);
        AddRowSeparators(Board.GridValues);
        AddColumnSeparators(Board.GridValues);
    }
    
    public void SetState(Cell cell, ShotResult shotResult)
    {
        var cellValue = ToBoardCellValue(shotResult);
        Board.GridValues[
            GetValueCellIndex(cell.RowIndex), 
            GetValueCellIndex(cell.ColumnIndex)] = cellValue;
    }
    
    private void AddTopLeftHeader(string?[,] gridValues)
    {
        gridValues[0, 0] = topLeftCellValue;
    }
    
    private void AddRowSeparators(string?[,] gridValues)
    {
        for (var rowIndex = 1; rowIndex < Board.Size; rowIndex += 2)
        {
            gridValues[rowIndex, 0] = firstColumnRowSeparator;
            for (var columnIndex = 1; columnIndex < Board.Size; columnIndex++)
            {
                gridValues[rowIndex, columnIndex] = RowSeparator;
            }
        }
    }
        
    private void AddColumnSeparators(string?[,] gridValues)
    {
        for (var columnIndex = 1; columnIndex < Board.Size; columnIndex += 2)
        {
            for (var rowIndex = 0; rowIndex < Board.Size; rowIndex += 2)
            {
                gridValues[rowIndex, columnIndex] = ColumnSeparator;
            }
        }
    }
    
    private void AddRowNumbers(string?[,] gridValues)
    {
        var rowNumber = FirstRowNumber;
        for (var rowIndex = HeaderColumnCount; rowIndex < Board.Size; rowIndex += 2)
        {
            gridValues[rowIndex, 0] = GetRowNumberString(rowNumber++);
        }
    }

    private string GetRowNumberString(int rowNumber)
    {
        return rowNumber
            .ToString()
            .PadLeft(rowColumnPaddingLength, PaddingChar);
    }

    private void SetColumnLetters(string?[,] gridValues)
    {
        var letterIndexInAlphabet = 0;
        for (var valueColumnIndex = HeaderColumnCount; valueColumnIndex < Board.Size; valueColumnIndex += 2)
        {
            gridValues[0, valueColumnIndex] = GetColumnLetter(letterIndexInAlphabet++);
        }
    }

    private static string GetColumnLetter(int letterIndexInAlphabet)
    {
        return ((char)(FirstColumnLetter + letterIndexInAlphabet))
            .ToString()
            .ToUpper();
    }
    
    private static string ToBoardCellValue(ShotResult shotResult)
    {
        return shotResult switch
        {
            ShotResult.Miss => ShotMiss,
            ShotResult.Hit => ShotHit,
            ShotResult.Sunk => ShotSunk,
            _ => throw new ArgumentOutOfRangeException(
                nameof(shotResult), 
                shotResult, 
                "Unknown shot result")
        };
    }
    
    private static int GetValueCellIndex(int valueCellIndex)
    {
        return valueCellIndex * 2 + HeaderColumnCount;
    }
}
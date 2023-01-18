namespace BattleshipsGameConsole.View.Output;

public static class ViewParameters
{
    public const char FirstColumnLetter = 'a';
    public const int FirstRowNumber = 1;
    
    public const int MaxLetterCode = 'z';
    public const int MaxRowNumber = 99;
    
    public const string ShotMiss = "\u2022"; // "●"
    public const string ShotHit = "\u22A1"; // "⊡"
    public const string ShotSunk = "\u22A0"; // "⊠"
    
    public const char TopLeftCellFillChar = '#';
    public const string ColumnSeparator = "|";
    public const char RowSeparatorChar = '-';
    public static readonly string RowSeparator = RowSeparatorChar.ToString();
}
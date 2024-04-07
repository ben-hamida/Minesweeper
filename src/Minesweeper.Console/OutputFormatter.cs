namespace Minesweeper.Console;

internal static class OutputFormatter
{
    public static string Format(
        Minefield minefield,
        TimeSpan elapsedTime,
        Coordinates activeCell)
    {
        var horizontalLine = new string('═', minefield.Width * 3);
        var formattedTime = $"{(int)elapsedTime.TotalMinutes}:{elapsedTime.Seconds:00}";
        return
            $"""
              --------------------------------------
             | Use arrow keys to navigate minefield |
             | Press space to uncover cell          |
             | Press F to flag/unflag cell          |
              --------------------------------------
                    🕑 {formattedTime}  🚩 {minefield.NumberOfRemainingFlags}              
               ╔{horizontalLine}╗
               {string.Join("\n  ", Enumerable
                   .Range(0, minefield.Height)
                   .Select(y => FormatRow(minefield, y, activeCell))
                   .Select(rowString => $"║ {rowString}║"))}
               ╚{horizontalLine}╝
                 {
                     minefield.GameState switch
                     {
                         GameState.Win => $"Congratulations, you won!\n    Your time: {formattedTime}",
                         GameState.Loss => "You lost ☹️",
                         _ => ""
                     }
                 }
             """;
    }

    private static string FormatRow(
        Minefield minefield,
        int rowIndex,
        Coordinates activeCell) =>
        string.Join(
            ' ',
            Enumerable
                .Range(0, minefield.Width)
                .Select(x => FormatCell(
                    cellState: minefield.GetCellState((x, rowIndex)),
                    isCurrentCell: activeCell == (x, rowIndex),
                    gameState: minefield.GameState)));

    private static string FormatCell(
        CellState cellState,
        bool isCurrentCell,
        GameState gameState) =>
        (cellState, isCurrentCell, gameState) switch
        {
            ({ HasBomb: true, IsUncovered: true }, _, _) => "💥",
            ({ HasBomb: true }, _, not GameState.Ongoing) => "💣",
            ({ IsFlagged: true }, _, _) => "🚩",
            (_, isCurrentCell: true, _) => "▢ ",
            ({ IsUncovered: false }, _, _) => "▧ ",
            ({ NumberOfAdjacentBombs: > 0 }, _, _) => cellState.NumberOfAdjacentBombs + " ",
            _ => "  "
        };
}
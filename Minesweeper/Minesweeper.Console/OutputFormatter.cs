namespace Minesweeper.Console;

internal static class OutputFormatter
{
    public static string Format(Minefield minefield, Coordinates activeCell)
    {
        var horizontalLine = new string('═', minefield.Width * 3);
        return
            $"""
              --------------------------------------
             | Use arrow keys to navigate minefield |
             | Press space to uncover cell          |
             | Press F to flag/unflag cell          |
              --------------------------------------
               ╔{horizontalLine}╗
               {string.Join("\n  ", Enumerable
                   .Range(0, minefield.Height)
                   .Select(y => FormatRow(minefield, y, activeCell))
                   .Select(rowString => $"║ {rowString}║"))}
               ╚{horizontalLine}╝
                 {
                     minefield.GameState switch
                     {
                         GameState.Win => "Congratulations, you won!",
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
                .Select(x => GetCell(
                    cellState: minefield.GetCellState((x, rowIndex)),
                    isCurrentCell: activeCell == (x, rowIndex),
                    gameState: minefield.GameState)));

    private static string GetCell(
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
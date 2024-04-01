namespace Minesweeper.Console;

using static ConsoleKey;

public enum InputType
{
    MoveToCell,
    UncoverCell,
    ToggleFlag
}

public static class InputParser
{
    public static (InputType Type, Coordinates Coordinates)? TryParseGameInput(
        ConsoleKey key,
        Minefield minefield,
        Coordinates activeCell) =>
        key switch
        {
            DownArrow when activeCell.Y + 1 < minefield.Height => activeCell.Move(deltaY: 1),
            UpArrow when activeCell.Y - 1 >= 0 => activeCell.Move(deltaY: -1),
            LeftArrow when activeCell.X - 1 >= 0 => activeCell.Move(deltaX: -1),
            RightArrow when activeCell.X + 1 < minefield.Width => activeCell.Move(deltaX: 1),
            Spacebar when minefield.GetCellState(activeCell) is { IsUncovered: false, IsFlagged: false } =>
                (InputType.UncoverCell, activeCell),
            F when minefield.GetCellState(activeCell) is { IsUncovered: false } =>
                (InputType.ToggleFlag, activeCell),
            _ => null
        };

    private static (InputType, Coordinates) Move(this Coordinates activeCell, int deltaY = 0, int deltaX = 0) =>
        (InputType.MoveToCell, (X: activeCell.X + deltaX, Y: activeCell.Y + deltaY));
}
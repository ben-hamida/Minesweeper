namespace Minesweeper;

internal static class Extensions
{
    public static void ValidateFieldSize(this MinefieldOptions options)
    {
        if (options.Width < 2 || options.Height < 2)
        {
            throw new ArgumentException("Invalid minefield size");
        }
    }

    public static IEnumerable<Coordinates> GenerateGameCoordinates(this MinefieldOptions options) =>
        from x in Enumerable.Range(0, options.Width)
        from y in Enumerable.Range(0, options.Height)
        select (x, y);

    public static void AssertCellCanBeUncovered(this CellState cellState)
    {
        if (cellState.IsUncovered)
        {
            throw new ArgumentException("Cell already uncovered");
        }

        if (cellState.IsFlagged)
        {
            throw new ArgumentException("Cannot uncover flagged cell");
        }
    }

    public static bool IsAllNonBombsUncovered(this IDictionary<Coordinates, CellState> cells) =>
        cells.Values.All(state => state.IsUncovered || state.HasBomb);

    public static bool IsAdjacent(this Coordinates coordinates, Coordinates otherCoordinate) =>
        coordinates != otherCoordinate &&
        Math.Abs(coordinates.X - otherCoordinate.X) <= 1 &&
        Math.Abs(coordinates.Y - otherCoordinate.Y) <= 1;
}
namespace Minesweeper;

public class Minefield
{
    private readonly MinefieldOptions _options;
    private readonly int _numberOfBombs;
    private readonly Dictionary<Coordinates, CellState> _cells;

    internal Minefield(MinefieldOptions options)
    {
        options.ValidateFieldSize();
        var gameCoordinates = options.GenerateGameCoordinates().ToHashSet();
        var bombCoordinates = options.BombPlacement.GetBombCoordinates(gameCoordinates);
        if (!bombCoordinates.IsSubsetOf(gameCoordinates))
        {
            throw new ArgumentException("Invalid bomb placement");
        }

        _numberOfBombs = bombCoordinates.Count;
        _options = options;
        _cells = gameCoordinates.ToDictionary(
            coordinates => coordinates,
            coordinates => new CellState(
                HasBomb: bombCoordinates.Contains(coordinates),
                NumberOfAdjacentBombs: bombCoordinates.Count(c => coordinates.IsAdjacent(c))));
    }

    public int Width => _options.Width;

    public int Height => _options.Height;

    public GameState GameState { get; private set; } = GameState.Ongoing;

    public int NumberOfRemainingFlags => _numberOfBombs - _cells.Values.Count(cell => cell.IsFlagged);

    public CellState GetCellState(Coordinates cellCoordinates) =>
        _cells.TryGetValue(cellCoordinates, out var cellState)
            ? cellState
            : throw new ArgumentException("Cell not found");

    internal void ToggleFlag(Coordinates cellCoordinate)
    {
        if (NumberOfRemainingFlags > 0)
        {
            GetCellState(cellCoordinate).ToggleFlag();
        }
    }

    internal void UncoverCell(Coordinates cellCoordinates)
    {
        var cellState = GetCellState(cellCoordinates);
        cellState.AssertCellCanBeUncovered();

        if (cellState.HasBomb)
        {
            cellState.SetUncovered();
            GameState = GameState.Loss;
            return;
        }

        UncoverCellsRecursively(cellCoordinates);
        GameState = _cells.IsAllNonBombsUncovered() ? GameState.Win : GameState.Ongoing;
    }

    private void UncoverCellsRecursively(Coordinates cellCoordinate)
    {
        var cellState = _cells[cellCoordinate];
        cellState.SetUncovered();
        if (cellState.NumberOfAdjacentBombs == 0)
        {
            _cells
                .Where(kvp => kvp.Key.IsAdjacent(cellCoordinate))
                .Where(kvp => !kvp.Value.IsUncovered)
                .ToList()
                .ForEach(kvp => UncoverCellsRecursively(kvp.Key));
        }
    }
}
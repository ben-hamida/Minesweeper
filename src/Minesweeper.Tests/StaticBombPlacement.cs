namespace Minesweeper.Tests;

public class StaticBombPlacement : IBombPlacement
{
    private readonly IReadOnlySet<Coordinates> _bombCoordinates;

    public StaticBombPlacement(IEnumerable<Coordinates> bombCoordinates) =>
        _bombCoordinates = bombCoordinates.ToHashSet();

    public IReadOnlySet<Coordinates> GetBombCoordinates(IReadOnlySet<Coordinates> gameCoordinates) =>
        _bombCoordinates;
}
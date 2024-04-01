namespace Minesweeper;

public interface IBombPlacement
{
    IReadOnlySet<Coordinates> GetBombCoordinates(IReadOnlySet<Coordinates> gameCoordinates);
}
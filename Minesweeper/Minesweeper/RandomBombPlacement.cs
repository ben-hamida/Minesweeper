namespace Minesweeper;

public class RandomBombPlacement : IBombPlacement
{
    private readonly int _numberOfBombs;

    public RandomBombPlacement(int numberOfBombs) =>
        _numberOfBombs = numberOfBombs > 0
            ? numberOfBombs
            : throw new ArgumentException("Number of bombs has to be a positive value", nameof(numberOfBombs));

    public IReadOnlySet<Coordinates> GetBombCoordinates(IReadOnlySet<Coordinates> gameCoordinates)
    {
        var bombCoordinatesCandidates = gameCoordinates.ToArray();
        Random.Shared.Shuffle(bombCoordinatesCandidates);
        return bombCoordinatesCandidates.Take(_numberOfBombs).ToHashSet();
    }
}
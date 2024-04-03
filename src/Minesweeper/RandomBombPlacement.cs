namespace Minesweeper;

public class RandomBombPlacement : IBombPlacement
{
    private readonly Random _random;
    private readonly int _numberOfBombs;

    public RandomBombPlacement(int numberOfBombs, int? seed = null)
    {
        _random = seed is null ? new Random() : new Random(seed.Value);
        _numberOfBombs = numberOfBombs > 0
            ? numberOfBombs
            : throw new ArgumentException("Number of bombs has to be a positive value", nameof(numberOfBombs));
    }

    public IReadOnlySet<Coordinates> GetBombCoordinates(IReadOnlySet<Coordinates> gameCoordinates)
    {
        var bombCoordinatesCandidates = gameCoordinates.ToArray();
        _random.Shuffle(bombCoordinatesCandidates);
        return bombCoordinatesCandidates.Take(_numberOfBombs).ToHashSet();
    }
}
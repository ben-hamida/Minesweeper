namespace Minesweeper.Tests;

public class RandomBombPlacementTests
{
    [Fact]
    public void ShouldProduceValidCoordinates()
    {
        // Arrange
        var bombPlacement = new RandomBombPlacement(5);
        var gameCoordinates = GenerateGameCoordinates(5, 5);

        // Test
        var bombCoordinates = bombPlacement.GetBombCoordinates(gameCoordinates);

        // Assert
        Assert.True(bombCoordinates.IsSubsetOf(gameCoordinates));
    }

    [Fact]
    public void ShouldProduceTheCorrectNumberOfBombCoordinates()
    {
        // Arrange
        const int numberOfBombs = 8;
        var bombPlacement = new RandomBombPlacement(numberOfBombs);
        var gameCoordinates = GenerateGameCoordinates(7, 10);

        // Test
        var bombCoordinates = bombPlacement.GetBombCoordinates(gameCoordinates);

        // Assert
        Assert.Equal(numberOfBombs, bombCoordinates.Count);
    }

    [Fact]
    public void ShouldProduceDifferentBombCoordinatesOnConsecutiveRuns()
    {
        // Arrange
        var bombPlacement = new RandomBombPlacement(10, seed: 0);
        var gameCoordinates = GenerateGameCoordinates(4, 4);

        // Test
        var firstBombCoordinates = bombPlacement.GetBombCoordinates(gameCoordinates);
        var secondBombCoordinates = bombPlacement.GetBombCoordinates(gameCoordinates);

        // Assert
        Assert.NotEqual(firstBombCoordinates, secondBombCoordinates);
    }

    [Fact]
    public void ShouldProduceSameCoordinatesOnConsecutiveRunsWhenUsingSameSeed()
    {
        // Arrange
        const int seed = 4;
        var bombPlacement1 = new RandomBombPlacement(10, seed);
        var bombPlacement2 = new RandomBombPlacement(10, seed);
        var gameCoordinates = GenerateGameCoordinates(8, 8);

        // Test
        var firstBombCoordinates = bombPlacement1.GetBombCoordinates(gameCoordinates);
        var secondBombCoordinates = bombPlacement2.GetBombCoordinates(gameCoordinates);

        // Assert
        Assert.Equal(firstBombCoordinates, secondBombCoordinates);
    }

    private static HashSet<Coordinates> GenerateGameCoordinates(int width, int height) => (
            from x in Enumerable.Range(0, width)
            from y in Enumerable.Range(0, height)
            select (x, y))
        .ToHashSet();
}
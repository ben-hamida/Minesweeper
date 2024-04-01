namespace Minesweeper.Tests;

public class MinefieldTests
{
    private static readonly Coordinates BombCoordinate = (4, 4);
    private readonly Minefield _minefield = new(
        new MinefieldOptions
        {
            Width = 5,
            Height = 5,
            BombPlacement = new StaticBombPlacement([BombCoordinate])
        });

    [Fact]
    public void UncoveringBombShouldResultInLoss()
    {
        _minefield.UncoverCell(BombCoordinate);

        Assert.Equal(GameState.Loss, _minefield.GameState);
    }

    [Fact]
    public void UncoveringAllNonBombCellsShouldResultInWin()
    {
        _minefield.UncoverCell((0, 0));

        Assert.Equal(GameState.Win, _minefield.GameState);
    }
}
namespace Minesweeper;

public class MinefieldOptions
{
    public int Width { get; init; } = 10;

    public int Height { get; init; } = 10;

    public IBombPlacement BombPlacement { get; init; } = new RandomBombPlacement(10);
}
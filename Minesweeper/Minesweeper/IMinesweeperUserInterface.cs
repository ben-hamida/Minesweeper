namespace Minesweeper;

public interface IMinesweeperUserInterface
{
    void Draw(Minefield minefield, TimeSpan elapsedTime);

    (Coordinates coordinates, CellAction action) GetNextInput(Minefield minefield);

    bool ShouldPlayNewGame();
}

public enum CellAction
{
    Uncover,
    ToggleFlag
}
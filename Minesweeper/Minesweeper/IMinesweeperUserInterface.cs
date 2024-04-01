namespace Minesweeper;

public interface IMinesweeperUserInterface
{
    void Draw(Minefield minefield);

    (Coordinates coordinates, CellAction action) GetNextInput(Minefield minefield);

    bool ShouldPlayNewGame();
}

public enum CellAction
{
    Uncover,
    ToggleFlag
}
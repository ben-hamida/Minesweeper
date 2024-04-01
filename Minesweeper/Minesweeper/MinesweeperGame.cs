namespace Minesweeper;

public class MinesweeperGame
{
    private readonly IMinesweeperUserInterface _userInterface;
    private readonly MinefieldOptions _options;

    private static readonly Dictionary<CellAction, Action<Minefield, Coordinates>> ActionMap = new()
    {
        { CellAction.Uncover, (minefield, cell) => minefield.UncoverCell(cell) },
        { CellAction.ToggleFlag, (minefield, cell) => minefield.ToggleFlag(cell) }
    };

    public MinesweeperGame(IMinesweeperUserInterface userInterface, MinefieldOptions options)
    {
        _userInterface = userInterface;
        _options = options;
    }

    public void Start()
    {
        var playNewGame = true;
        while (playNewGame)
        {
            PlayGame();
            playNewGame = _userInterface.ShouldPlayNewGame();
        }
    }

    private void PlayGame()
    {
        var minefield = new Minefield(_options);

        while (minefield.GameState is GameState.Ongoing)
        {
            _userInterface.Draw(minefield);
            var (coordinates, action) = _userInterface.GetNextInput(minefield);
            ActionMap[action](minefield, coordinates);
        }

        _userInterface.Draw(minefield);
    }
}
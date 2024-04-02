using System.ComponentModel;

namespace Minesweeper.Console;

using Console = System.Console;
using static ConsoleKey;

internal class ConsoleMinesweeperInterface : IMinesweeperUserInterface
{
    private Coordinates _activeCell = (0, 0);
    private TimeSpan _elapsedTime = TimeSpan.Zero;

    public void Draw(Minefield minefield, TimeSpan elapsedTime)
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine(OutputFormatter.Format(minefield, elapsedTime, _activeCell));
        _elapsedTime = elapsedTime;
    }

    public (Coordinates coordinates, CellAction action) GetNextInput(Minefield minefield)
    {
        while (true)
        {
            var input = InputParser.TryParseGameInput(
                Console.ReadKey(intercept: true).Key,
                minefield,
                _activeCell);

            switch (input?.Type)
            {
                case null: continue;
                case InputType.UncoverCell: return (input.Value.Coordinates, CellAction.Uncover);
                case InputType.ToggleFlag: return (input.Value.Coordinates, CellAction.ToggleFlag);
                case InputType.MoveToCell:
                    _activeCell = input.Value.Coordinates;
                    Draw(minefield, _elapsedTime);
                    break;
                default: throw new InvalidEnumArgumentException();
            }
        }
    }

    public bool ShouldPlayNewGame()
    {
        Console.WriteLine("    New game? (Y/N)");

        while (true)
        {
            var key = Console.ReadKey(intercept: true).Key;
            switch (key)
            {
                case Y:
                    _activeCell = (0, 0);
                    return true;
                case N:
                    Console.WriteLine("    Exiting...");
                    return false;
            }
        }
    }
}
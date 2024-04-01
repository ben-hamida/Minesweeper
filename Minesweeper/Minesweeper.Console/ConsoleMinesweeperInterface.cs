using System.ComponentModel;

namespace Minesweeper.Console;

using Console = System.Console;
using static ConsoleKey;

internal class ConsoleMinesweeperInterface : IMinesweeperUserInterface
{
    private Coordinates _activeCell = (0, 0);

    public void Draw(Minefield minefield)
    {
        Console.Clear();
        Console.WriteLine(OutputFormatter.Format(minefield, _activeCell));
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
                    Draw(minefield);
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
                    Console.WriteLine("Exiting...");
                    return false;
            }
        }
    }
}
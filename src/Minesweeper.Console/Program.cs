global using Coordinates = (int X, int Y);

using System.Text;
using Minesweeper;
using Minesweeper.Console;

Console.CursorVisible = false;
Console.OutputEncoding = Encoding.Unicode;
Console.Clear();

var minefieldOptions = new MinefieldOptions
{
    Width = 10,
    Height = 10,
    BombPlacement = new RandomBombPlacement(15)
};

var game = new MinesweeperGame(
    new ConsoleMinesweeperInterface(),
    minefieldOptions);

game.Start();
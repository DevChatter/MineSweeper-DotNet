// See https://aka.ms/new-console-template for more information

using Domain;

Console.WriteLine("Hello, World!");

var game = new Game();
game.Start(10, Difficulty.Elf);

for (int i = 0; i < 10; i++)
{
    for (int j = 0; j < 10; j++)
    {
        int num = game.Grid._grid[i,j];
        Console.Write(num < 0 ? "x" : num == 0 ? ' ' : num.ToString());
    }
    Console.WriteLine();
}

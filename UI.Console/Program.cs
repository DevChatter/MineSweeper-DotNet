// See https://aka.ms/new-console-template for more information

using Domain;

internal class Program
{
    private const int SideSize = 5;

    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var game = new Game();
        game.Start(SideSize, Difficulty.Easy);

        while (game.State == GameState.Started)
        {
            DisplayGrid(game);
            var toReveal = GetSelection();
            game.Reveal(toReveal.x, toReveal.y);
        }
        DisplayGrid(game);
        if (game.State == GameState.Won)
        {
            Console.WriteLine("Congratulations!");
        }
        else
        {
            Console.WriteLine("Better luck next time!");
        }
    }
    static (int x,int y) GetSelection()
    {
        (int, int) selection = (-1,-1);
        do
        {
            Console.WriteLine("Please choose an index (0,0) is top left:");
            string rawInput = Console.ReadLine() ?? "";
            try
            {
                string[] parts = rawInput.Split(new string[] { ",", ", ", " ", " , " }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                selection = (int.Parse(parts[0]), int.Parse(parts[1]));
            }
            catch (Exception)
            {
                // Gulp
            }
        } while (IsNotValid(selection));
        return selection;
    }

    private static bool IsNotValid((int x, int y) selection)
    {
        return selection.x < 0 || selection.x >=  SideSize || selection.y < 0 || selection.y >= SideSize;
    }

    private static void DisplayGrid(Game game)
    {
        Console.WriteLine($"Bombs Remaining: {game.Grid.RemainingBombCount}");
        for (int i = 0; i < game.Grid.Cells.GetLength(0); i++)
        {
            var chars = new string[game.Grid.Cells.GetLength(1)];
            for (int j = 0; j < chars.Length; j++)
            {
                chars[j] = game.Grid.Cells[i, j].DisplayText;
            }
            Console.WriteLine(string.Join('|', chars));
            Console.WriteLine(string.Join('+', Enumerable.Repeat("-", chars.Length)));
        }
        Console.WriteLine();
    }

}
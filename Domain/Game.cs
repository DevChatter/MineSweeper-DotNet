using System.Collections.Generic;

namespace Domain;

public class Game
{
    public Grid Grid { get; set; } = new (0, Difficulty.Elf);
    public GameState State { get; set; } = GameState.NotStarted;

    public void Start(ushort size, Difficulty difficulty)
    {
        Grid = new(size, difficulty);
        State = GameState.Started;
    }

    public void Reveal(int rowIndex, int columnIndex, HashSet<(int x, int y)>? done = null)
    {
        if (State != GameState.Started) return;

        Cell? cell = Grid.SafeGet(rowIndex, columnIndex);
        if (cell == null) return;

        cell.Reveal();
        if (cell.IsGift)
        {
            State = GameState.Lost;
        }
        else if (Grid.OnlyGiftsLeft())
        {
            State = GameState.Won;
        }
        else if (cell == 0)
        {
            done ??= new HashSet<(int x, int y)>();
            RevealNeighbors((rowIndex,columnIndex), done);
        }
    }

    private void RevealNeighbors((int x, int y) coords, HashSet<(int x, int y)> done)
    {
        if (done.Add(coords))
        {
            Reveal(coords.x - 1, coords.y - 1, done);
            Reveal(coords.x + 0, coords.y - 1, done);
            Reveal(coords.x + 1, coords.y - 1, done);
            Reveal(coords.x + 1, coords.y + 0, done);
            Reveal(coords.x + 1, coords.y + 1, done);
            Reveal(coords.x + 0, coords.y + 1, done);
            Reveal(coords.x - 1, coords.y + 1, done);
            Reveal(coords.x - 1, coords.y + 0, done);
        }

    }
}
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

    public void Reveal(int rowIndex, int columnIndex)
    {
        if (State != GameState.Started)
        {
            return;
        }

        Cell cell = Grid.Cells[rowIndex, columnIndex];
        cell.Reveal();
        if (cell.IsGift)
        {
            State = GameState.Lost;
        }
        else if (Grid.OnlyGiftsLeft())
        {
            State = GameState.Won;
        }
    }
}
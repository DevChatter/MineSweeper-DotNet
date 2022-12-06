namespace Domain;

public class Game
{
    public Grid Grid { get; set; } = new (0, Difficulty.Elf);
    public GameState State { get; set; }

    public void Start(ushort size, Difficulty difficulty)
    {
        Grid = new(size, difficulty);
    }

    public void Reveal(int rowIndex, int columnIndex)
    {

    }
}
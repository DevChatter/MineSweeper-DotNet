public class Game
{
    public Grid Grid { get; set; } = new Grid(0, Difficulty.Elf);

    public void Start(ushort size, Difficulty difficulty)
    {
        Grid = new(size, difficulty);
    }
}

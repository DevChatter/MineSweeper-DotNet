namespace Domain;

public class Cell
{
    public int Number { get; private set; } = 0;
    public bool Revealed { get; private set; } = false;
    public bool IsGift => Number < 0;

    internal void Reveal()
    {
        Revealed = true;
    }

    public static Cell operator ++(Cell x)
    {
        x.Number += 1;
        return x;
    }

    public static implicit operator int(Cell x) => x.Number;
    public static implicit operator Cell(int number) => new() { Number = number };
}

using Domain;

namespace UnitTests;

public class StartingNewGameShould
{
    private readonly Game _game = new ();
    
    [Theory]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(50)]
    public void CreatesGridOfCorrectSize(ushort size)
    {
        _game.Start(size, Difficulty.Easy);
        // TODO: Test something
    }
}

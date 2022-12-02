namespace UnitTests;

public class StartingNewGameShould
{
    private Game _game = new Game();
    
    [Theory]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(50)]
    public void CreatesGridOfCorrectSize(ushort size)
    {
        _game.Start(size, Difficulty.Reindeer);

        Assert.Equal(size * size, _game.Grid._grid.Length);
    }
}

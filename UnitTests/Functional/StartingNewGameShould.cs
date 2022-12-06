namespace UnitTests.Functional;

public class TestClass1
{
    private readonly Game _game = new ();
    
    [Fact]
    public void Test1()
    {
        _game.Start(4, Difficulty.Reindeer);
        for (int rowIndex = 0; rowIndex < 4; rowIndex++)
        {
            for (int colIndex = 0; colIndex < 4; colIndex++)
            {
                if (!_game.Grid.IsGift(rowIndex, colIndex))
                {
                    _game.Reveal(rowIndex, colIndex);
                }
            }
        }

        _game.State.Should().Be(GameState.Won);
    }
}

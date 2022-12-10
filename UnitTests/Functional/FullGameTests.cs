namespace UnitTests.Functional;

public class FullGameTests
{
    private readonly Game _game = new();

    [Fact]
    public void ClearAllSpacesSafely()
    {
        _game.State.Should().Be(GameState.NotStarted);
        _game.Start(4, Difficulty.Reindeer);
        _game.State.Should().Be(GameState.Started);

        for (int rowIndex = 0; rowIndex < 4; rowIndex++)
        {
            for (int colIndex = 0; colIndex < 4; colIndex++)
            {
                if (!_game.Grid.Cells[rowIndex, colIndex].IsGift)
                {
                    _game.Reveal(rowIndex, colIndex);
                }
            }
        }

        _game.State.Should().Be(GameState.Won);
    }

    [Fact]
    public void ClickBombFirst()
    {
        _game.State.Should().Be(GameState.NotStarted);
        _game.Start(4, Difficulty.Reindeer);
        _game.State.Should().Be(GameState.Started);
        ClickBmob();

        _game.State.Should().Be(GameState.Lost);

        void ClickBmob()
        {
            for (int rowIndex = 0; rowIndex < 4; rowIndex++)
            {
                for (int colIndex = 0; colIndex < 4; colIndex++)
                {
                    if (_game.Grid.Cells[rowIndex, colIndex].IsGift)
                    {
                        _game.Reveal(rowIndex, colIndex);
                        return;
                    }
                }
            }
        }
    }
}

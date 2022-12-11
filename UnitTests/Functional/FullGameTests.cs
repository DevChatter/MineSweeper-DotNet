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

        ClickAllNonBombs();

        _game.State.Should().Be(GameState.Won);
    }

    [Fact]
    public void ClickBombFirst()
    {
        _game.State.Should().Be(GameState.NotStarted);
        _game.Start(4, Difficulty.Reindeer);
        _game.State.Should().Be(GameState.Started);
        ClickBomb();

        _game.State.Should().Be(GameState.Lost);
    }

    [Fact]
    public void GameUnplayableAfterBombCLick()
    {
        _game.State.Should().Be(GameState.NotStarted);
        _game.Start(4, Difficulty.Reindeer);
        _game.State.Should().Be(GameState.Started);
        ClickBomb();
        _game.State.Should().Be(GameState.Lost);
        ClickAllNonBombs();
        _game.State.Should().Be(GameState.Lost);
        _game.Grid.Cells.Count(cell => !cell.Revealed).Should().BeGreaterThan(0);
    }

    private void ClickAllNonBombs()
    {
        for (int rowIndex = 0; rowIndex < 4; rowIndex++)
        {
            for (int colIndex = 0; colIndex < 4; colIndex++)
            {
                if (!_game.Grid.Cells[rowIndex, colIndex].IsBomb)
                {
                    _game.Reveal(rowIndex, colIndex);
                }
            }
        }
    }

    private void ClickBomb()
    {
        for (int rowIndex = 0; rowIndex < 4; rowIndex++)
        {
            for (int colIndex = 0; colIndex < 4; colIndex++)
            {
                if (_game.Grid.Cells[rowIndex, colIndex].IsBomb)
                {
                    _game.Reveal(rowIndex, colIndex);
                    return;
                }
            }
        }
    }
}

public class Grid
{
    private int _size;
    public int[,] _grid { get; set; } = new int[0, 0];
    private Random _random = new Random();

    public Grid(ushort size, Difficulty difficulty)
    {
        _size = size;

        _grid = new int[size,size];

        FillWithGifts(difficulty);

        SetHintValues(size);
    }

    private void FillWithGifts(Difficulty difficulty)
    {
        int giftCount = (int)(_grid.Length * ((double)difficulty / 100));
        var possibles = Enumerable.Range(0, _grid.Length).ToArray();
        var locations = possibles.OrderBy(x => _random.Next()).Take(giftCount);

        foreach (var index in locations)
        {
            _grid[index % _size, index / _size] = _random.Next(-5, 0);
        }
    }


    public bool IsGift(int rowIndex, int colIndex)
    {
        return ((int)_grid.GetValue(rowIndex, colIndex)) < 0;
    }

    private void SetHintValues(ushort size)
    {
        for (int rowIndex = 0; rowIndex < size; rowIndex++)
        {
            for (int colIndex = 0; colIndex < size; colIndex++)
            {
                if (IsGift(rowIndex, colIndex))
                {
                    SafeIncrement(rowIndex-1, colIndex);
                    SafeIncrement(rowIndex+1, colIndex);
                    SafeIncrement(rowIndex, colIndex-1);
                    SafeIncrement(rowIndex, colIndex+1);

                    SafeIncrement(rowIndex-1, colIndex-1);
                    SafeIncrement(rowIndex+1, colIndex+1);
                    SafeIncrement(rowIndex+1, colIndex-1);
                    SafeIncrement(rowIndex-1, colIndex+1);
                }
            }
        }
    }

    private void SafeIncrement(int rowIndex, int colIndex)
    {
        if (rowIndex < 0
            || colIndex < 0
            || rowIndex >= _size
            || colIndex >= _size)
        {
            return;
        }

        _grid[rowIndex, colIndex]++;
    }
}

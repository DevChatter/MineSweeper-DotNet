using Microsoft.VisualBasic.CompilerServices;
using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain;

public class Grid
{
    private readonly int _size;
    public readonly int[,] _grid; // TODO: Encapsulate
    private readonly Random _random = new Random();

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

public class Cell
{
    public int Count { get; set; } = 0;
    public bool Revealed { get; set; } = false;

    public static Cell operator ++(Cell x)
    {
        x.Count += 1;
        return x;
    }

    public static implicit operator int(Cell x) => x.Count;
    public static implicit operator Cell(int number) => new() { Count = number };
}

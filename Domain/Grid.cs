using Microsoft.VisualBasic.CompilerServices;
using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain;

public class Grid
{
    public int RemainingBombCount => Cells.Count(cell => cell.Number < 0);
    public int StartingGiftCount { get; }
    public int TotalSpaceCount => Cells.Length;
    public int RevealedSpaceCount => Cells.Count(cell => cell.Revealed);

    private readonly int _size;
    private Difficulty _difficulty;
    public Cell[,] Cells { get; }
    private readonly Random _random = new Random();

    public Grid(ushort size, Difficulty difficulty)
    {
        _size = size;
        _difficulty = difficulty;
        Cells = CreateCells(size);

        StartingGiftCount = (int)(Cells.Length * ((double)_difficulty / 100));

        FillWithGifts();

        SetHintValues(size);


    }

    private static Cell[,] CreateCells(ushort size)
    {
        Cell[,] cells = new Cell[size, size];
        for (int rowIndex = 0; rowIndex < cells.GetLength(0); rowIndex++)
        {
            for (int colIndex = 0; colIndex < cells.GetLength(1); colIndex++)
            {
                cells[rowIndex, colIndex] = new Cell();
            }
        }
        return cells;
    }

    private void FillWithGifts()
    {
        var possibles = Enumerable.Range(0, Cells.Length).ToArray();
        var locations = possibles.OrderBy(x => _random.Next()).Take(StartingGiftCount);

        foreach (var index in locations)
        {
            Cells[index % _size, index / _size] = -1;
        }
    }


    private void SetHintValues(ushort size)
    {
        for (int rowIndex = 0; rowIndex < size; rowIndex++)
        {
            for (int colIndex = 0; colIndex < size; colIndex++)
            {
                if (Cells[rowIndex, colIndex].IsGift)
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
        if (IsOutOfBounds(rowIndex, colIndex))
        {
            return;
        }

        Cells[rowIndex, colIndex]++;
    }

    public Cell? SafeGet(int rowIndex, int colIndex)
    {
        if (IsOutOfBounds(rowIndex, colIndex))
        {
            return null;
        }
        return Cells[rowIndex, colIndex];
    }

    private bool IsOutOfBounds(int rowIndex, int colIndex)
    {
        return rowIndex < 0
                    || colIndex < 0
                    || rowIndex >= _size
                    || colIndex >= _size;
    }

    internal bool OnlyGiftsLeft()
    {
        return Cells.Where(x => !x.Revealed).All(x => x.IsGift);
    }
}

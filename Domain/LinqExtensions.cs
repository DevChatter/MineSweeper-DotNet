using Ardalis.GuardClauses;

namespace Domain;

public static class MultidimensionalArrayExtensions
{
    public static int Count<T>(this T[,] source, Func<T, bool> predicate)
    {
        Guard.Against.Null(source, nameof(source));
        Guard.Against.Null(predicate, nameof(predicate));

        return source.Cast<T>().Count(predicate);
    }

    public static IEnumerable<T> Where<T>(this T[,] source, Func<T, bool> predicate)
    {
        Guard.Against.Null(source, nameof(source));
        Guard.Against.Null(predicate, nameof(predicate));

        return source.Cast<T>().Where<T>(predicate);
    }
}

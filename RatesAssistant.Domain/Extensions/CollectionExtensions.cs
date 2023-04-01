namespace RatesAssistant.Domain.Extensions;

public static class CollectionExtensions
{
    /// <summary>
    /// Splits the given collection in two according to <paramref name="predicate"/>
    /// </summary>
    public static (List<T> satisfying, List<T> notSatisfying) Partition<T>(
        this IEnumerable<T> source, Func<T, bool> predicate)
    {
        var satisfying = new List<T>();
        var notSatisfying = new List<T>();

        foreach (var item in source)
        {
            if (predicate(item))
            {
                satisfying.Add(item);
            }
            else
            {
                notSatisfying.Add(item);
            }
        }

        return (satisfying, notSatisfying);
    }
}

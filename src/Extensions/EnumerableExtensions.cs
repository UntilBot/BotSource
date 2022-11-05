namespace Until.Extensions;

public static class EnumerableExtensions
{
	public static IEnumerable<T> Times<T>(this IEnumerable<T> source, in int n)
	{
		List<T> temp = new();
		foreach (T item in source)
			for (int i = 0; i < n; i++)
				temp.Add(item);
		return temp;
	}
}

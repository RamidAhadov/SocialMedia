namespace Core.Utilities.CombineData;

public static class CombineLists
{
    public static List<T> Combine<T>(params List<T>[] lists)
    {
        List<T> combinedList = new List<T>();

        foreach (var list in lists)
        {
            combinedList.AddRange(list);
        }

        return combinedList;
    }
}
namespace Core.Utilities.CombineData;

public static class CombineData
{
    public static string CombineInteger(int number1, int number2)
    {
        int smallInt = Math.Min(number1, number2);
        int bigInt = Math.Max(number1, number2);

        string result = $"{smallInt}-{bigInt}";

        return result;
    }
}
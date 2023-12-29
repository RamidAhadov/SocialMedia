namespace Core.Utilities.TimeSpan;

public static class TimeSpanCalculator
{
    public static string CalculateDifference(DateTime date)
    {
        System.TimeSpan? difference = DateTime.Now - date;
        int days = difference.Value.Days;
        int hours = difference.Value.Hours;
        int minutes = difference.Value.Minutes;
        int seconds = difference.Value.Seconds;
        var dateTime = date.ToString("yyyy MMMM dd");
        if (days >= 1)
        {
            return dateTime;
        }

        if (hours is > 1 and < 23)
        {
            return $"{hours} hours ago";
        }

        if (hours == 1)
        {
            return "An hour ago";
        }

        if (minutes is > 1 and < 59)
        {
            return $"{minutes} minutes ago";
        }

        if (minutes < 1)
        {
            return "Just now";
        }

        return "NaN";
    }
}
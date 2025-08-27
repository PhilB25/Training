namespace AT_API.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            var now = DateTime.UtcNow;
            int age = now.Year - dateTimeOffset.Year;
            if (now < dateTimeOffset.AddYears(age))
            {
                age--;
            }
            return age;
        }

    }
}

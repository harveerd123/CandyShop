namespace CandyShop
{
    internal static class Helpers
    {
        internal static int GetDaysSinceOpening()
        {
            var openingDate = new DateTime(2023, 1, 1);
            TimeSpan timeDifference = DateTime.Now - openingDate;

            return timeDifference.Days;
        }
    }
}

namespace DateCalculation;

public class DateCalculationService
{
    private const string DAY_UNIT = "day";
    private const string DAYS_UNIT = "days";
    private const string WEEK_UNIT = "week";
    private const string WEEKS_UNIT = "weeks";
    private const string MONTH_UNIT = "month";
    private const string MONTHS_UNIT = "months";
    private const string YEAR_UNIT = "year";
    private const string YEARS_UNIT = "years";

    public string? CalculateAge(DateTime? dateOfBirth, DateTime? currentDate) =>
        !currentDate.HasValue || !dateOfBirth.HasValue || currentDate.Value < dateOfBirth.Value
            ? null
            : CalculateAgeWithUnits(dateOfBirth.Value, currentDate.Value);

    public string? CalculateExactAge(DateTime? dateOfBirth, DateTime? currentDate) =>
        !currentDate.HasValue || !dateOfBirth.HasValue || currentDate.Value < dateOfBirth.Value
            ? null
            : CalculateExactAgeWithUnits(dateOfBirth.Value, currentDate.Value);

    private static string CalculateAgeWithUnits(DateTime birthDate, DateTime currentDate)
    {
        var totalDays = (currentDate - birthDate).Days;

        var monthUpperDayLimitCalculation =
            (DateTime.IsLeapYear(birthDate.Year) && birthDate.Month < 3 && (currentDate.Month < 2 || (currentDate.Month == 2 && currentDate.Day > 28)))
            || (DateTime.IsLeapYear(currentDate.Year) && (currentDate.Month > 2 || (currentDate.Month == 2 && currentDate.Day > 28)))
                ? 366
                : 365;

        if (totalDays < 7)
        {
            return $"{totalDays} {(totalDays == 1 ? DAY_UNIT : DAYS_UNIT)}";
        }

        if (totalDays is >= 28 && totalDays < monthUpperDayLimitCalculation && (currentDate.Month != birthDate.Month || currentDate.Year != birthDate.Year))
        {
            var totalMonths = currentDate.Month - birthDate.Month;

            if (totalMonths <= 0)
            {
                totalMonths += 12;
            }

            if (currentDate.Day < birthDate.Day)
            {
                totalMonths--;
            }

            if (totalMonths <= 0)
            {
                var weeksDifference = totalDays / 7;
                return $"{weeksDifference} {(weeksDifference == 1 ? WEEK_UNIT : WEEKS_UNIT)}";
            }

            return $"{totalMonths} {(totalMonths == 1 ? MONTH_UNIT : MONTHS_UNIT)}";
        }
        else if (totalDays is >= 7 and <= 30)
        {
            var weeksDifference = totalDays / 7;
            return $"{weeksDifference} {(weeksDifference == 1 ? WEEK_UNIT : WEEKS_UNIT)}";
        }
        else
        {
            var years = CalculateAgeInYears(birthDate, currentDate);
            return $"{years} {(years.TotalYears == 1 ? YEAR_UNIT : YEARS_UNIT)}";
        }
    }

    private static string CalculateExactAgeWithUnits(DateTime birthDate, DateTime currentDate)
    {
        var years = CalculateAgeInYears(birthDate, currentDate);

        var remainingDays = years.RemainingDays;

        var totalYearsResult = $"{years.TotalYears} {(years.TotalYears == 1 ? YEAR_UNIT : YEARS_UNIT)}";
        var totalMonthsResult = $"0 {MONTHS_UNIT}";
        var totalWeeksResult = $"0 {WEEKS_UNIT}";
        var totalDaysResult = $"0 {DAYS_UNIT}";

        if (remainingDays < 7)
        {
            totalDaysResult = $"{remainingDays} {(remainingDays == 1 ? DAY_UNIT : DAYS_UNIT)}";
            return $"{totalYearsResult}, {totalMonthsResult}, {totalWeeksResult}, {totalDaysResult}";
        }

        var monthUpperDayLimitCalculation =
            (DateTime.IsLeapYear(birthDate.Year) && birthDate.Month < 3 && (currentDate.Month < 2 || (currentDate.Month == 2 && currentDate.Day > 28)))
            || (DateTime.IsLeapYear(currentDate.Year) && (currentDate.Month > 2 || (currentDate.Month == 2 && currentDate.Day > 28)))
                ? 366
                : 365;

        // TODO: Start figuring out how to accuratley subtract months and weeks and days when months have different days and on different years there could be more days.
        if (remainingDays is >= 28 && remainingDays < monthUpperDayLimitCalculation && (currentDate.Month != birthDate.Month || currentDate.Year != birthDate.Year))
        {
            var totalMonths = currentDate.Month - birthDate.Month;

            if (totalMonths <= 0)
            {
                totalMonths += 12;
            }

            if (currentDate.Day < birthDate.Day)
            {
                totalMonths--;
            }

            if (totalMonths <= 0)
            {
                var weeksDifference = remainingDays / 7;
                remainingDays -= weeksDifference * 7;
                totalWeeksResult = $"{weeksDifference} {(weeksDifference == 1 ? WEEK_UNIT : WEEKS_UNIT)}";
            }

            totalMonthsResult = $"{totalMonths} {(totalMonths == 1 ? MONTH_UNIT : MONTHS_UNIT)}";
        }

        if (remainingDays is >= 7 and <= 30)
        {
            var weeksDifference = remainingDays / 7;
            remainingDays -= weeksDifference * 7;
            totalWeeksResult = $"{weeksDifference} {(weeksDifference == 1 ? WEEK_UNIT : WEEKS_UNIT)}";
        }

        if (remainingDays != 0)
        {
            totalDaysResult = $"{remainingDays} {(remainingDays == 1 ? DAY_UNIT : DAYS_UNIT)}";
        }

        return $"{totalYearsResult}, {totalMonthsResult}, {totalWeeksResult}, {totalDaysResult}";
    }

    private static (int TotalYears, int RemainingDays) CalculateAgeInYears(DateTime birthDate, DateTime currentDate)
    {
        var dateYearCount = birthDate;

        var totalDays = (currentDate - birthDate).Days;
        var years = 0;
        var remainingDays = totalDays;

        if (remainingDays > 364)
        {
            while (true)
            {
                var incrementDateYearCount = dateYearCount.AddYears(1);

                if (birthDate.Month == 2 && birthDate.Day == 29 && DateTime.IsLeapYear(incrementDateYearCount.Year))
                {
                    incrementDateYearCount = incrementDateYearCount.AddDays(1);
                }

                var daysInYear =
                    (DateTime.IsLeapYear(dateYearCount.Year) && dateYearCount.Month < 3 && (incrementDateYearCount.Month < 2 || (incrementDateYearCount.Month == 2 && incrementDateYearCount.Day > 28)))
                    || (DateTime.IsLeapYear(incrementDateYearCount.Year) && (incrementDateYearCount.Month > 2 || (incrementDateYearCount.Month == 2 && incrementDateYearCount.Day > 28)))
                        ? 366
                        : 365;

                if (remainingDays >= daysInYear)
                {
                    remainingDays -= daysInYear;
                    years++;

                    dateYearCount = dateYearCount.AddYears(1);

                    if (birthDate.Month == 2 && birthDate.Day == 29 && DateTime.IsLeapYear(dateYearCount.Year))
                    {
                        dateYearCount = dateYearCount.AddDays(1);
                    }
                }
                else
                {
                    break;
                }
            }
        }
        return (years, remainingDays);
    }
}
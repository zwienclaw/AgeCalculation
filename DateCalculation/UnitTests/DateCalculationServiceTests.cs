using DateCalculation;

namespace UnitTests;

public class DateCalculationServiceTests
{
    private DateTime _currentDateLeapYear;
    private DateTime _specificDateLeapYear;
    private DateCalculationService _service;

    [SetUp]
    public void Setup()
    {
        _service = new DateCalculationService();
        _currentDateLeapYear = new DateTime(2024, 2, 29);
        _specificDateLeapYear = _currentDateLeapYear.AddYears(-1);
    }

    [Test]
    public void CalculateAge_BirthYear2021Age1_Returns1Year() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2021, 2, 21),
            currentDate: new DateTime(2022, 2, 21),
            expectedResult: "1 year");

    [Test]
    public void LeapYearDayBirth_BirthYear2024Age100_ReturnsOneHundredYears() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2024, 2, 29),
            currentDate: new DateTime(2124, 2, 29),
            expectedResult: "100 years");

    [Test]
    public void LeapYearDayBirth_BirthYear2024Age99_ReturnsNinetyNineYears() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2024, 2, 29),
            currentDate: new DateTime(2124, 2, 28),
            expectedResult: "99 years");

    [Test]
    public void LeapYearDayBirth_BirthYear2024Age100AndOneDay_Returns100Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2024, 2, 29),
            currentDate: new DateTime(2124, 3, 1),
            expectedResult: "100 years");

    [Test]
    public void LeapYearDayBirth_BirthYear2096_Returns4Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2096, 2, 29),
            currentDate: new DateTime(2100, 2, 28),
            expectedResult: "4 years");

    [Test]
    public void BirthYear2096Age3_IncludesNonLeapYearLeapYear_Returns3Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2096, 3, 1),
            currentDate: new DateTime(2100, 2, 28),
            expectedResult: "3 years");

    [Test]
    public void LeapYearDayBirth_BirthYear2050Age100AndOneDay_Returns100Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2050, 2, 28),
            currentDate: new DateTime(2150, 2, 28),
            expectedResult: "100 years");

    [Test]
    public void LeapYearDayBirth_BirthYear1972Age99_Returns99Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(1972, 2, 29),
            currentDate: new DateTime(2072, 2, 28),
            expectedResult: "99 years");

    [Test]
    public void LeapYearDayBirth_BirthYear1972Age100_Returns100Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(1972, 2, 29),
            currentDate: new DateTime(2072, 2, 29),
            expectedResult: "100 years");

    [Test]
    public void LeapYearDayBirth_AgeDivisibleBy400_Returns400Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2000, 2, 29),
            currentDate: new DateTime(2400, 2, 29),
            expectedResult: "400 years");

    [Test]
    public void LeapYearDayBirth_BirthYear2000_Returns100Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2000, 2, 29),
            currentDate: new DateTime(2100, 2, 28),
            expectedResult: "100 years");

    [Test]
    public void LeapYearDayBirth_Birth1996Feb29_CurrentDate2100Feb28_Returns104Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(1996, 2, 29),
            currentDate: new DateTime(2100, 2, 28),
            expectedResult: "104 years");

    [Test]
    public void LeapYearDayBirth_Birth1996Feb29_CurrentDate2104Feb29_Returns108Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(1996, 2, 29),
            currentDate: new DateTime(2104, 2, 29),
            expectedResult: "108 years");

    [Test]
    public void LeapYearDayBirth_Age800_Returns800Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2000, 2, 29),
            currentDate: new DateTime(2800, 2, 29),
            expectedResult: "800 years");

    [Test]
    public void LeapYearDayBirth_Birth2000Feb29_CurrentDate2800Feb28_Returns799Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2000, 2, 29),
            currentDate: new DateTime(2800, 2, 28),
            expectedResult: "799 years");

    [Test]
    public void LeapYearDayBirth_Age796_Returns796Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2000, 2, 29),
            currentDate: new DateTime(2796, 2, 29),
            expectedResult: "796 years");

    [Test]
    public void LeapYearDayBirth_Birth2000Feb29_CurrentDate2799Feb28_Returns799Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2000, 2, 29),
            currentDate: new DateTime(2799, 2, 28),
            expectedResult: "799 years");

    [Test]
    public void LeapYearDayBirth_Age800AndOneDay_Returns800Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2000, 2, 29),
            currentDate: new DateTime(2800, 3, 1),
            expectedResult: "800 years");

    [Test]
    public void Birth1999Mar1_CurrentDate2800Feb28_Returns800Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(1999, 3, 1),
            currentDate: new DateTime(2800, 2, 28),
            expectedResult: "800 years");

    [Test]
    public void LeapYearDayBirth_Age804_Returns804Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2000, 2, 29),
            currentDate: new DateTime(2804, 2, 29),
            expectedResult: "804 years");

    [Test]
    public void LeapYearDayBirth_Age4000_Returns4000Years() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2000, 2, 29),
            currentDate: new DateTime(6000, 2, 29),
            expectedResult: "4000 years");

    [Test]
    public void AgeAtSpecificDate_CurrentDate_DOBNull_ReturnsNull()
    {
        DateTime? dateOfBirth = null;
        var result = _service.CalculateAge(dateOfBirth, DateTime.Today);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void DateOfBirthGreaterThanFromDate_ReturnsNull()
    {
        DateTime? dateOfBirth = DateTime.Today.AddDays(1);
        var result = _service.CalculateAge(dateOfBirth, DateTime.Today);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void AgeOnSameDay_CurrentDate_ReturnsZeroDays() => ExecuteAgeCalculationTestCase(0, DateTime.Today, DateTime.Today, "0 days");

    [Test]
    public void AgeAtOneDay_CurrentDate_ReturnsOneDay() => ExecuteAgeCalculationTestCase(-1, DateTime.Today, DateTime.Today, "1 day");

    [Test]
    public void AgeAtSixDays_CurrentDate_ReturnsSixDays() => ExecuteAgeCalculationTestCase(-6, DateTime.Today, DateTime.Today, "6 days");

    [Test]
    public void AgeAtOneWeek_CurrentDate_ReturnsOneWeek() => ExecuteAgeCalculationTestCase(-7, DateTime.Today, DateTime.Today, "1 week");

    [Test]
    public void AgeAt13Days_CurrentDate_ReturnsOneWeek() => ExecuteAgeCalculationTestCase(-13, DateTime.Today, DateTime.Today, "1 week");

    [Test]
    public void AgeAtThreeWeeks_CurrentDate_ReturnsThreeWeeks() => ExecuteAgeCalculationTestCase(-27, DateTime.Today, DateTime.Today, "3 weeks");

    [Test]
    public void BirthDate2023Feb1_CurrentDate2023Mar1_Returns1Month() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2023, 2, 1),
            currentDate: new DateTime(2023, 3, 1),
            expectedResult: "1 month");

    [Test]
    public void BirthDate2023Feb1_CurrentDate2024Jan31_Returns1Month() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2023, 2, 28),
            currentDate: new DateTime(2024, 1, 31),
            expectedResult: "11 months");

    [Test]
    public void BirthDate2023Feb1_CurrentDate2023Mar31_Returns1Month() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2023, 2, 1),
            currentDate: new DateTime(2023, 3, 31),
            expectedResult: "1 month");

    [Test]
    public void BirthDate2023Feb1_CurrentDate2023Apr1_Returns2Months() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2023, 2, 1),
            currentDate: new DateTime(2023, 4, 1),
            expectedResult: "2 months");

    [Test]
    public void BirthDate2023Dec1_CurrentDate2024Jan1_Returns1Month() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2023, 12, 1),
            currentDate: new DateTime(2024, 1, 1),
            expectedResult: "1 month");

    [Test]
    public void BirthDate2023Dec1_CurrentDate2024Jan31_Returns1Month() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2023, 12, 1),
            currentDate: new DateTime(2024, 1, 31),
            expectedResult: "1 month");

    [Test]
    public void BirthDate2023Dec1_CurrentDate2024Feb1_Returns2Months() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2023, 12, 1),
            currentDate: new DateTime(2024, 2, 1),
            expectedResult: "2 months");

    [Test]
    public void BirthDate2023Dec1_CurrentDate2023Dec31_Returns4Weeks() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2023, 12, 1),
            currentDate: new DateTime(2023, 12, 31),
            expectedResult: "4 weeks");

    [Test]
    public void BirthDate2023Dec1_CurrentDate2023Dec14_Returns1Week() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2023, 12, 1),
            currentDate: new DateTime(2023, 12, 14),
            expectedResult: "1 week");

    [Test]
    public void BirthDate2023Jan1_CurrentDate2024Jan1_Returns1Year() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2023, 1, 1),
            currentDate: new DateTime(2024, 1, 1),
            expectedResult: "1 year");

    [Test]
    public void BirthDate2024Jan1_CurrentDate2025Jan1_Returns1Year() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2024, 1, 1),
            currentDate: new DateTime(2025, 1, 1),
            expectedResult: "1 year");

    [Test]
    public void BirthDate2024Jan1_CurrentDate2024Dec31_Returns11Months() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2024, 1, 1),
            currentDate: new DateTime(2024, 12, 31),
            expectedResult: "11 months");

    [Test]
    public void BirthDate2023Jan31_CurrentDate2023Feb28_Returns4Weeks() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2023, 1, 31),
            currentDate: new DateTime(2023, 2, 28),
            expectedResult: "4 weeks");

    [Test]
    public void BirthDate2023Jan31_CurrentDate2023Mar1_Returns1Month() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2023, 1, 31),
            currentDate: new DateTime(2023, 3, 1),
            expectedResult: "1 month");

    [Test]
    public void BirthDate2024Jan31_CurrentDate2024Feb28_Returns4Weeks() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2024, 1, 31),
            currentDate: new DateTime(2024, 2, 28),
            expectedResult: "4 weeks");

    [Test]
    public void BirthDate2024Jan31_CurrentDate2024Mar1_Returns1Month() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2024, 1, 31),
            currentDate: new DateTime(2024, 3, 1),
            expectedResult: "1 month");

    [Test]
    public void BirthDate202Jan31_CurrentDate2024Jan31_Returns1Year() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2023, 1, 31),
            currentDate: new DateTime(2024, 1, 31),
            expectedResult: "1 year");

    [Test]
    public void BirthDate2024Jan31_CurrentDate2025Jan31_Returns1Year() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2024, 1, 31),
            currentDate: new DateTime(2025, 1, 31),
            expectedResult: "1 year");

    [Test]
    public void BirthDate2024Jan31_CurrentDate2025Jan30_Returns11Months() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2024, 1, 31),
            currentDate: new DateTime(2025, 1, 30),
            expectedResult: "11 months");

    [Test]
    public void BirthDate2024Feb1_CurrentDate2024Feb29_Returns4Weeks() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2024, 2, 1),
            currentDate: new DateTime(2024, 2, 29),
            expectedResult: "4 weeks");

    [Test]
    public void BirthDate2024Jan1_CurrentDate2024Feb29_Returns1Month() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2024, 1, 1),
            currentDate: new DateTime(2024, 2, 29),
            expectedResult: "1 month");

    [Test]
    public void BirthDate2024Mar1_CurrentDate2024Mar31_Returns4Weeks() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2024, 3, 1),
            currentDate: new DateTime(2024, 3, 31),
            expectedResult: "4 weeks");

    [Test]
    public void BirthDate2024Feb1_CurrentDate2024Mar31_Returns1Month() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2024, 2, 1),
            currentDate: new DateTime(2024, 3, 31),
            expectedResult: "1 month");

    [Test]
    public void BirthDate2024Feb29_CurrentDate2025Feb28_Returns1Year() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2024, 2, 29),
            currentDate: new DateTime(2025, 2, 28),
            expectedResult: "1 year");

    [Test]
    public void BirthDate2024Feb29_CurrentDate2025Feb27_Returns11Months() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2024, 2, 29),
            currentDate: new DateTime(2025, 2, 27),
            expectedResult: "11 months");

    [Test]
    public void BirthDate2022Mar14_CurrentDate2023Mar13_Returns11Months() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2022, 3, 14),
            currentDate: new DateTime(2023, 3, 13),
            expectedResult: "11 months");

    [Test]
    public void LeapYearBirth_BirthYear2020_CurrentDate2022Feb28_Returns1Year() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2020, 2, 29),
            currentDate: new DateTime(2022, 2, 28),
            expectedResult: "2 years");

    [Test]
    public void BirthDate2023Mar1_CurrentDate2024Feb29_Returns11Months() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2023, 3, 1),
            currentDate: new DateTime(2024, 2, 29),
            expectedResult: "11 months");

    [Test]
    public void BirthDate2022Mar1_CurrentDate2023Feb28_Returns11Months() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2022, 3, 1),
            currentDate: new DateTime(2023, 2, 28),
            expectedResult: "11 months");

    [Test]
    public void AgeAtTwoYears_CurrentDateLeapYear_ReturnsTwoYears() => ExecuteAgeCalculationTestCase(-800, _currentDateLeapYear, _currentDateLeapYear, "2 years");

    [Test]
    public void BirthDate2022Feb28_CurrentDate2023Feb28_Returns1Year() =>
        ExecuteAgeCalculationTestCase(
            dateOfBirth: new DateTime(2022, 2, 28),
            currentDate: new DateTime(2023, 2, 28),
            expectedResult: "1 year");

    [Test]
    public void AgeAtTwoYears_YearAgoLeapYear_ReturnsTwoYears() => ExecuteAgeCalculationTestCase(-1095, _currentDateLeapYear, _specificDateLeapYear, "1 year");

    private void ExecuteAgeCalculationTestCase(int daysToAdjustDob, DateTime startingDate, DateTime checkDate, string expectedResult)
    {
        var dateOfBirth = startingDate.AddDays(daysToAdjustDob);
        var result = _service.CalculateAge(dateOfBirth, checkDate);
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    private void ExecuteAgeCalculationTestCase(DateTime dateOfBirth, DateTime currentDate, string expectedResult)
    {
        var result = _service.CalculateAge(dateOfBirth, currentDate);
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
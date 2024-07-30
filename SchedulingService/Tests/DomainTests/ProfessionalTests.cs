using Domain.Entities;
using Domain.Exceptions;

namespace DomainTests;

public class ProfessionalTests
{
    [SetUp]
    public void Setup()
    {
    }
    [TestCase("09:00:00", "12:00:00", 0)]
    [TestCase("09:00:00", "14:00:00", 1)]
    [TestCase("07:00:00", "12:00:00", 2)]
    [TestCase("09:00:00", "12:00:00", 3)]
    public void ShouldAddTheDayOfWorkToUserIfTheDayAndTimesAreDiferents(
    string timeInit, string timeEnd, int dayOfWeek
        )
    {
        var professionalId = Guid.NewGuid();
        var day1 = new DayOfWork
        {
            ProfessionalId = professionalId,
            DayOfWeek = DayOfWeek.Sunday,
            TimeInit = TimeSpan.Parse("09:00:00"),
            TimeEnd = TimeSpan.Parse("14:00:00"),
        };
        var day2 = new DayOfWork
        {
            ProfessionalId = professionalId,
            DayOfWeek = (DayOfWeek)dayOfWeek,
            TimeInit = TimeSpan.Parse(timeInit),
            TimeEnd = TimeSpan.Parse(timeEnd),
        };

        var professional = new Professional
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Email = "test@email",
            PasswordHash = "hash",
        };
        professional.AddNewDayOfWork(day1);
        professional.AddNewDayOfWork(day2);
        Assert.AreEqual(2, professional.DaysOfWork.Count);
    }
    [Test]
    public void ShouldNotAddTheSameDayOfWorkToUser()
    {
        var professionalId = Guid.NewGuid();
        var day1 = new DayOfWork
        {
            ProfessionalId = professionalId,
            DayOfWeek = DayOfWeek.Sunday,
            TimeInit = TimeSpan.Parse("09:00:00"),
            TimeEnd = TimeSpan.Parse("14:00:00"),
        };
        var day2 = new DayOfWork
        {
            ProfessionalId = professionalId,
            DayOfWeek = DayOfWeek.Sunday,
            TimeInit = TimeSpan.Parse("09:00:00"),
            TimeEnd = TimeSpan.Parse("14:00:00"),
        };

        var professional = new Professional
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Email = "test@email",
            PasswordHash = "hash",
        };
        professional.AddNewDayOfWork(day1);
        Assert.Throws<DayOfWorkAlreadyAddToUserException>(() =>
        {
            professional.AddNewDayOfWork(day2);
        });
    }
}
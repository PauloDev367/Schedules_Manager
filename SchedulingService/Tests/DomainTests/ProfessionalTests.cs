using Domain.Entities;
using Domain.Exceptions;

namespace DomainTests;

public class ProfessionalTests
{
    [SetUp]
    public void Setup()
    {
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
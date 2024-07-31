using Domain.Entities;
using Domain.Exceptions;
using System;

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
    
    [TestCase("2024-07-30 08:00:00", 0)]
    [TestCase("2024-08-30 17:00:00", 1)]
    [TestCase("2024-09-30 16:00:00", 2)]
    [TestCase("2024-10-30 20:00:00", 3)]
 
    public void ShouldNotAddNewScheduleIfProfessionalDoesntWorkInTheDayAndTime(
        string scheduleDate, int scheduleDayOfWeek
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
            DayOfWeek = DayOfWeek.Sunday,
            TimeInit = TimeSpan.Parse("09:00:00"),
            TimeEnd = TimeSpan.Parse("15:00:00"),
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

        var scheduleDateParse = DateTime.Parse(scheduleDate);
        var today = DateTime.Now;
        var scheduleDateTest = DateTime.Parse($"{today.Year}-{today.Month}-{today.Day} {scheduleDateParse.Hour}:{scheduleDateParse.Minute}:{scheduleDateParse.Second}");
        Console.WriteLine(scheduleDateTest.ToString());
        var schedule = new Schedule
        {
            Date = scheduleDateTest,
            DayOfWeek = (DayOfWeek)scheduleDayOfWeek,
            IsImportant = true,
            Title = "Test",
        };
        schedule.AddProfessional(professional);

        var error = Assert.Throws<InvalidScheduleDayOfWorkException>(() =>
        {
            professional.AddNewSchedule(schedule);
        });
    }
    [Test]
    public void ShouldAddNewScheduleIfProfessionalWorkInTheScheduleDate()
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
            TimeEnd = TimeSpan.Parse("15:00:00"),
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
        var today = DateTime.Now;

        var scheduleDate = DateTime.Parse($"{today.Year}-{today.Month}-{today.Day} 10:00:00");
        scheduleDate.AddDays(1);
        var schedule = new Schedule
        {
            Date = scheduleDate,
            DayOfWeek = DayOfWeek.Sunday,
            IsImportant = true,
            Title = "Test",
        };
        schedule.AddProfessional(professional);

        Assert.Throws<InvalidScheduleDayOfWorkException>(() =>
        {
            professional.AddNewSchedule(schedule);
        });
    }

}
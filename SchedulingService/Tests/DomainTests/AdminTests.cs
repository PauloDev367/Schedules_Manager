using Domain.Entities;

namespace DomainTests;

public class AdminTests
{
    [Test]
    public void ShouldAddNewScheduleToProfessional()
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
            Id = professionalId,
            Name = "Test",
            Email = "test@email",
            PasswordHash = "hash",
        };
        professional.AddNewDayOfWork(day1);
        professional.AddNewDayOfWork(day2);
        
        var today = DateTime.Now;

        var scheduleDate = DateTime.Parse($"{today.Year}-{today.Month}-{today.Day} 10:00:00");
        scheduleDate= scheduleDate.AddDays(1);
        
        var schedule = new Schedule
        {
            Date = scheduleDate,
            DayOfWeek = DayOfWeek.Sunday,
            IsImportant = true,
            Title = "Test",
            Time =  TimeSpan.Parse("10:00:00")
        };
        schedule.AddProfessional(professional);
        
        var admin = new Admin { Id = Guid.NewGuid() };
        admin.AddNewEventToProfessional(professional, schedule);
        var expectedSchedulesQuantity = 1;
        Assert.AreEqual(professional.Schedules.Count, expectedSchedulesQuantity);
    }
}
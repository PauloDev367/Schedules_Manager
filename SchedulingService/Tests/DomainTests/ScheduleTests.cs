using Domain.Entities;

namespace DomainTests;

public class ScheduleTests
{
    [Test]
    public void ShouldAddScheduleProfessional()
    {
        var profId = Guid.NewGuid();
        var professional = new Professional { Id = profId };
        var schedule = new Schedule();
        schedule.AddProfessional(professional);
        
        Assert.AreEqual(profId, schedule.ProfessionalId);
    }
}
namespace Domain.Entities;
public class DayOfWork
{
    public Guid Id { get; set; }
    public Guid ProfessionalId { get; set; }
    public Professional Professional { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan TimeInit { get; set; }
    public TimeSpan TimeEnd { get; set; }
}

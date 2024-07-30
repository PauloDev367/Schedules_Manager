namespace Domain.Entities;
public class Schedule
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan Time { get; set; }
    public DateTime Date { get; set; }
    public bool IsImportant { get; set; }
    public Guid ProfessionalId { get; private set; }
    public string Comment { get; set; }
    public void AddProfessional(Professional professional)
    { ProfessionalId = professional.Id; }
}

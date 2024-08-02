using Domain.Enums;

namespace Domain.Entities;
public class Admin : User
{
    public Roles Role { get; set; } = Roles.Admin;

    public void AddNewEventToProfessional(Professional professional, Schedule schedule)
    {
        professional.AddNewSchedule(schedule);
    }
    
}

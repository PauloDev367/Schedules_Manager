using Domain.Enums;

namespace Domain.Entities;
public class Admin : User
{
    public Admin()
    {
        Role = Enums.Roles.Admin;
    }

    public void AddNewEventToProfessional(Professional professional, Schedule schedule)
    {
        professional.AddNewSchedule(schedule);
    }
    
}

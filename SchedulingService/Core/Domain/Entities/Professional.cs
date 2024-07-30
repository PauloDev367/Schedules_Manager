using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities;
public class Professional : User
{
    public Roles Role { get; set; } = Roles.Professional;
    public List<DayOfWork> DaysOfWork { get; set; } = new List<DayOfWork>();
    public List<Schedule> Schedules { get; set; } = new List<Schedule>();

    public void AddNewDayOfWork(DayOfWork dayOfWork)
    {
        var day = DaysOfWork.Find(d =>
        {
            if (
                d.DayOfWeek.Equals(dayOfWork.DayOfWeek) &&
                d.TimeInit.Equals(dayOfWork.TimeInit) &&
                d.TimeEnd.Equals(dayOfWork.TimeEnd)

                )
            {
                return true;
            }
            return false;
        });
        if (day != null)
        {
            throw new DayOfWorkAlreadyAddToUserException("This day of work was already registered");
        }
        DaysOfWork.Add(dayOfWork);
    }

}

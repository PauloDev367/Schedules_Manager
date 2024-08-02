using Domain.Enums;
using Domain.Exceptions;
using System;

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
    public void AddNewSchedule(Schedule schedule)
    {
        if (schedule.Date < DateTime.Now)
            throw new InvalidScheduleDayOfWorkException("The schedule date should be greater than now");

        var day = DaysOfWork
            .FirstOrDefault(d => d.DayOfWeek == schedule.DayOfWeek 
                                 && d.ProfessionalId == schedule.ProfessionalId);
        
        if (day != null)
        {
            if (!(schedule.Time >= day.TimeInit && schedule.Time <= day.TimeEnd))
                throw new InvalidScheduleDayOfWorkException("This professional doesn't work this day");
        }
        else
        {
            throw new InvalidScheduleDayOfWorkException("No working hours defined for this professional on this day");
        }

        Schedules.Add(schedule);
    }
}

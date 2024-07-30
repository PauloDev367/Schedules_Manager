namespace Domain.Exceptions;
public class DayOfWorkAlreadyAddToUserException : Exception
{
    public DayOfWorkAlreadyAddToUserException(string? message) : base(message)
    {
    }
}

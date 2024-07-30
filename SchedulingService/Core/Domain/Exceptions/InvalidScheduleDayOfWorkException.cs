namespace Domain.Exceptions;
public class InvalidScheduleDayOfWorkException : Exception
{
    public InvalidScheduleDayOfWorkException(string? message) : base(message)
    {
    }
}

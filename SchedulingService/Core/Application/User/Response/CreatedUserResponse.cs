namespace Application.User.Response;
public class CreatedUserResponse
{
    public UserDto User { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    public void AddError(string message) => Errors.Add(message);

    public void AddErrors(List<string> errors)
    {
        foreach (var message in errors)
            Errors.Add(message);
    }
    
}

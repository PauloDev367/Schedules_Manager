namespace Application.User.Response;
public class CreatedUserResponse
{
    public UserDto User { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
}

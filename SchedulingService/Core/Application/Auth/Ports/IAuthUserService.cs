using Application.Auth.Request;
using Application.Auth.Response;
using Application.User.Response;

namespace Application.Auth.Ports;

public interface IAuthUserService
{
    public Task<LoggedUserResponse> AuthenticateAsync(LoginUserRequest request);
    public Task<RegisteredUserResponse> RegisterAsync(RegisterUserRequest register);
    public Task UpdateAuthUserAsync(Domain.Entities.User user, UpdateUserRequest request);
    public Task DeleteAsync(Domain.Entities.User user);
    public Task<UserDto?> GetOneByIdAsync(Guid id);
}
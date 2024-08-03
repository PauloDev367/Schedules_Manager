using Application.Auth.Ports;
using Application.Auth.Request;
using Application.Auth.Response;
using Application.User.Response;

namespace IdentityAuth;

public class IdentityAuthService : IAuthUserService
{
    public Task<LoggedUserResponse> AuthenticateAsync(LoginUserRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<RegisteredUserResponse> RegisterAsync(RegisterUserRequest register)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAuthUserAsync(Domain.Entities.User user, UpdateUserRequest request)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Domain.Entities.User user)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto?> GetOneByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
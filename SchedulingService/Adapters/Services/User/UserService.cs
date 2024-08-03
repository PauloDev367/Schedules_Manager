using Application.User.Ports;
using Application.User.Request;
using Application.User.Response;

namespace Services.User;

public class UserService : IUserService
{
    public Task<UserDto> CreateUserAsync(CreateUserRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserDto>> GetAllAsync(GetUsersRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto?> GetOneAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> UpdateBasicInfosAsync(UpdateUserBasicInfosRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> UpdatePasswordAsync(UpdateUserPasswordRequest request)
    {
        throw new NotImplementedException();
    }
}
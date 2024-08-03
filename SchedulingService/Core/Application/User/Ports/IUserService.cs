using Application.User.Request;
using Application.User.Response;

namespace Application.User.Ports;

public interface IUserService
{
    public Task<UserDto> CreateUserAsync(CreateUserRequest request);
    public Task<List<UserDto>> GetAllAsync(GetUsersRequest request);
    public Task<UserDto?> GetOneAsync(Guid id);
    public Task DeleteAsync(Guid id);
    public Task<UserDto> UpdateBasicInfosAsync(UpdateUserBasicInfosRequest request);
    public Task<UserDto> UpdatePasswordAsync(UpdateUserPasswordRequest request);
}
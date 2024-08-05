using Application.Auth.Ports;
using Application.Auth.Request;
using Application.User.Ports;
using Application.User.Request;
using Application.User.Response;
using Domain.Enums;
using Domain.Ports;
using System.ComponentModel.DataAnnotations;

namespace Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthUserService _authUserService;
    public UserService(IUserRepository userRepository, IAuthUserService authUserService)
    {
        _userRepository = userRepository;
        _authUserService = authUserService;
    }

    public async Task<CreatedUserResponse> CreateUserAsync(CreateUserRequest request)
    {
        var response = new CreatedUserResponse();
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(request, null, null);
        bool valid = Validator.TryValidateObject(request, validationContext, validationResults, true);

        if (!valid)
        {
            foreach (var result in validationResults)
                response.Errors.Add(result.ErrorMessage);
            return response;
        }


        Domain.Entities.User user;
        if (request.Roles.Equals(Roles.Professional))
        {
            user = new Domain.Entities.Professional
            {
                Name = request.Name,
                Role = request.Roles,
                Email = request.Email,
            };
        }
        else
        {
            user = new Domain.Entities.Admin
            {
                Name = request.Name,
                Role = request.Roles,
                Email = request.Email,
            };
        }
        user.SetPassword(request.Password);
        var register = new RegisterUserRequest
        {
            Email = request.Email,
            Password = request.Password,
            Role = request.Roles,
        };

        var authRegister = await _authUserService.RegisterAsync(register);
        if (authRegister.Errors.Count < 0)
        {
            var created = await _userRepository.CreateAsync(user);
            response.User = new UserDto(created);                
        }
        else
        {
            response.AddErrors(authRegister.Errors);
        }

        return response;
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
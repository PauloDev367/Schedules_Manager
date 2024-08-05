using Application.Auth.Ports;
using Application.Auth.Request;
using Application.Auth.Response;
using Application.User.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports;
using Moq;
using Services.User;

namespace ApplicationTests;

public class UserServiceTests
{
    private Mock<IUserRepository> _userRepository;
    private Mock<IAuthUserService> _authUserService;

    [SetUp]
    public void Setup()
    {
        _authUserService = new Mock<IAuthUserService>();
        _userRepository = new Mock<IUserRepository>();
    }

    [Test]
    public async Task ShouldCreateNewProfessionalUser()
    {
        var createdUserId = Guid.NewGuid();
        var request = new CreateUserRequest
        {
            Email = "email@email.com",
            Name = "Nome",
            Password = "Senha@123123",
            ConfirmPassword = "Senha@123123"
        };
        request.SetRole(Roles.Professional);

        _authUserService.Setup(au => au.RegisterAsync(It.IsAny<RegisterUserRequest>()))
            .ReturnsAsync(new RegisteredUserResponse());
        _userRepository.Setup(us => us.CreateAsync(It.IsAny<User>()))
            .ReturnsAsync(new Professional { Id = createdUserId, Name = "Nome", Email = "email@email.com" });
        var userService = new UserService(_userRepository.Object, _authUserService.Object);
        var created = await userService.CreateUserAsync(request);
        var errorsQuantityExpected = 0;
        Assert.AreEqual(created.User.Id, createdUserId);
        Assert.AreEqual(created.User.Role, Roles.Professional.ToString());
        Assert.AreEqual(created.Errors.Count, errorsQuantityExpected);
    }
    [Test]
    public async Task ShouldNotCreateNewProfessionalUserIfTheValuesIsNotSetCorrectly()
    {
        var createdUserId = Guid.NewGuid();
        var request = new CreateUserRequest
        {
            Email = "email@email",
            Name = "Nome",
            Password = "S123123",
            ConfirmPassword = "Senha@123123"
        };
        request.SetRole(Roles.Professional);

        _authUserService.Setup(au => au.RegisterAsync(It.IsAny<RegisterUserRequest>()))
            .ReturnsAsync(new RegisteredUserResponse());
        _userRepository.Setup(us => us.CreateAsync(It.IsAny<User>()))
            .ReturnsAsync(new Professional { Id = createdUserId, Name = "Nome", Email = "email@email.com" });
        var userService = new UserService(_userRepository.Object, _authUserService.Object);
        var created = await userService.CreateUserAsync(request);
        Assert.AreNotEqual(0, created.Errors.Count);
    }
    [Test]
    public async Task ShouldNotCreateNewProfessionalUserIfAuthServiceReturnErrors()
    {
        var createdUserId = Guid.NewGuid();
        var request = new CreateUserRequest
        {
            Email = "email@email.com",
            Name = "Nome",
            Password = "Senha@123123",
            ConfirmPassword = "Senha@123123"
        };
        request.SetRole(Roles.Professional);
        var authResponse = new RegisteredUserResponse();
        authResponse.AddErrors("Error 1");
        
        _authUserService.Setup(au => au.RegisterAsync(It.IsAny<RegisterUserRequest>()))
            .ReturnsAsync(authResponse);
        _userRepository.Setup(us => us.CreateAsync(It.IsAny<User>()))
            .ReturnsAsync(new Professional { Id = createdUserId, Name = "Nome", Email = "email@email.com" });
        var userService = new UserService(_userRepository.Object, _authUserService.Object);
        var created = await userService.CreateUserAsync(request);
        var errorsQuantityExpected = 1;
        
        Assert.AreEqual(created.User, null);
        Assert.AreEqual(created.Errors.Count, errorsQuantityExpected);
    }

    [Test]
    public async Task ShouldCreateNewAdminUser()
    {
        var createdUserId = Guid.NewGuid();
        var request = new CreateUserRequest
        {
            Email = "email@email.com",
            Name = "Nome",
            Password = "Senha@123123",
            ConfirmPassword = "Senha@123123"
        };
        request.SetRole(Roles.Admin);

        _authUserService.Setup(au => au.RegisterAsync(It.IsAny<RegisterUserRequest>()))
            .ReturnsAsync(new RegisteredUserResponse());
        _userRepository.Setup(us => us.CreateAsync(It.IsAny<User>()))
            .ReturnsAsync(new Admin { Id = createdUserId, Name = "Nome", Email = "email@email.com"});
        
        var userService = new UserService(_userRepository.Object, _authUserService.Object);
        var created = await userService.CreateUserAsync(request);
        var errorsQuantityExpected = 0;
        
        Assert.AreEqual(Roles.Admin.ToString(), created.User.Role);
        Assert.AreEqual(created.User.Id, createdUserId);
        Assert.AreEqual(created.Errors.Count, errorsQuantityExpected);
    }
}
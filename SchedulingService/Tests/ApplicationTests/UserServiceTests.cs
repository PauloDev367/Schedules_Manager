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

        Assert.AreEqual(created.User.Id, createdUserId);
    }
}
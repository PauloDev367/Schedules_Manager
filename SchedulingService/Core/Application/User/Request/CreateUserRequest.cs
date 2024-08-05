using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Application.User.Attributes;
using Domain.Enums;

namespace Application.User.Request;

public class CreateUserRequest
{
    [Required]
    [MinLength(3)]
    public string Name { get; set; }
    [Required]
    [PasswordValidation]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [JsonIgnore]
    public Roles Roles { get; private set; }

    public void SetRole(Roles role) => this.Roles = role;
}
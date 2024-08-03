using System.ComponentModel.DataAnnotations;
using Application.User.Attributes;

namespace Application.User.Request;

public class UpdateUserPasswordRequest
{
    [Required]
    [PasswordValidation]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}
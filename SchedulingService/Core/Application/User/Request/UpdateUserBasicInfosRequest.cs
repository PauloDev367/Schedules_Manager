using System.ComponentModel.DataAnnotations;

namespace Application.User.Request;

public class UpdateUserBasicInfosRequest
{
    [Required(AllowEmptyStrings = true)]
    [MinLength(3)]
    public string Name { get; set; }
    [Required(AllowEmptyStrings = true)]
    [EmailAddress]
    public string Email { get; set; } 
}
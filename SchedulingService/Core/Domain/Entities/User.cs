using Domain.Enums;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Entities;
public abstract class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PasswordHash { get; private set; }
    public string Email { get; set; }
    public Roles Role { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public void SetPassword(string password)
    {
        string sha256Hash = ComputeSha256Hash(password);
        PasswordHash = sha256Hash;
    }
    private string ComputeSha256Hash(string rawData)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}

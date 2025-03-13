using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class AppUser
{
    
    public int Id { get; init; }
    
    [MaxLength(255)]
    public required string UserName { get; init; }
    
    public required byte[] PasswordHash { get; init; }
    
    public required byte[] PasswordSalt { get; init; }
}
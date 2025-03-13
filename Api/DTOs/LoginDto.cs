using System.ComponentModel.DataAnnotations;

namespace Api.DTOs;

public class LoginDto
{
    
    [MaxLength(255)]
    public required string Username { get; set; }
    
    public required string Password { get; set; }
}
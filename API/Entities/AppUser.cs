using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class AppUser
{
    public int Id { get; set; }
    
    [StringLength(100)]
    public required string UserName { get; set; }
    
    
}
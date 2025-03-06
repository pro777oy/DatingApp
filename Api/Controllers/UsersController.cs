using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UsersController(DataContext context) : ControllerBase
{
    
    private readonly DataContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        
        return Ok(users);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(int id)
    {
        var user =  await _context.Users.FindAsync(id);
        

        return Ok(user);
    }
}
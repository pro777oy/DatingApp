using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

public class UsersController(DataContext context) : BaseApiController
{
    
    private readonly DataContext _context = context;
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        
        return Ok(users);
    }
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(int id)
    {
        var user =  await _context.Users.FindAsync(id);
        

        return Ok(user);
    }
}
using System.Security.Cryptography;
using System.Text;
using Api.Data;
using Api.DTOs;
using Api.Interfaces;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")]

    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if(await UserExists(registerDto.Username)) return BadRequest("Username already exists");
        using var hmac = new HMACSHA512(); 

        var user = new AppUser
        {
            UserName = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.GenerateToken(user)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user= await context.Users.FirstOrDefaultAsync(x =>
            x.UserName == loginDto.Username.ToLower());

        if (user==null) return Unauthorized("Username or password is incorrect");
        
        using var hmac = new HMACSHA512(user.PasswordSalt);
        
        var computedHash= hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (var i = 0; i < computedHash.Length; i++)
        {
            if(computedHash[i]!=user.PasswordHash[i]) return Unauthorized("Invalid Password");
        }
        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.GenerateToken(user)
        };
    }
    
    private async Task<bool> UserExists(string username)
    {
        var user= await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        return user;
    }
    
}
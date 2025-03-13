using Api.Models;

namespace Api.Interfaces;

public interface ITokenService
{
    string GenerateToken(AppUser user);
}
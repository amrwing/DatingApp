namespace API.Services;
using API.Entities;

public interface ITokenService
{
    public Task<string> CreateToken(AppUser user);
}
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> RegisterAsync(DTOs.RegisterRequest request)
    {
        if (await UserExistsAsync(request.Username))
        {
            return BadRequest("Username already registered");
        } //SI NO ENCUENTRA COINICIDENCIAS, MANDA 
        //UN BAD REQUEST
        using var hmac = new HMACSHA512();
        var user = new AppUser
        {
            UserName = request.Username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
            PasswordSalt = hmac.Key
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }
    private async Task<bool> UserExistsAsync(string username) =>
        await context.Users.AnyAsync(
            u => u.UserName.ToLower() == username.ToLower()
        ); //PERMITE BUSCAR LA PRIMER COINCIDENCIA EN NOMBRES DE USUARIO
    [HttpPost("login")]
    public async Task<ActionResult<AppUser>> LoginAsync(DTOs.LoginRequest request)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == request.Username.ToLower());
        if (user == null)
            return Unauthorized("Invalid username or ");

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        for (int i = 0; i < computeHash.Length; i++)
        {
            if (computeHash[i] != user.PasswordHash[i])
            {
                return Unauthorized("Invalid  or password");

            }
        }
        return user;
    }
}

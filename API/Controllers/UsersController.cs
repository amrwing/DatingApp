
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }
    //api/v1/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsersAsync()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }
    //api/v1/Users/2
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppUser>> GetUsersByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if(user==null) return NotFound();
        return user;
}

    [HttpGet("{name}")]
    public ActionResult<string> Ready(string name)
    {
        return $"Hi {name}";
}
}
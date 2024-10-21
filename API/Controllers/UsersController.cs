namespace API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _repository;

    private readonly IMapper _mapper;

    public UsersController(UserRepository repository, IMapper mapper) {
        _repository = repository;
        _mapper = mapper;
        }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();
    var response = _mapper.Map<IEnumerable<MemberResponse>>(users);
        return Ok(users);
    }

    [Authorize]
    [HttpGet("{id:int}")] // api/users/2
    public async Task<ActionResult<MemberResponse>> GetByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return  _mapper.Map<MemberResponse>(user);
    }
    
    [HttpGet("{username}")] // api/users/Calamardo
    public async Task<ActionResult<MemberResponse>> GetByIdAsync(string username)
    {
        var user = await _repository.GetByUsernameAsync(username);

        if (user == null)
        {
            return NotFound();
        }

       return  _mapper.Map<MemberResponse>(user);
    }
}
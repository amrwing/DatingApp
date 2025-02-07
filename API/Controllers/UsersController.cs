namespace API.Controllers;

using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _repository;
    private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;


     public UsersController(IUserRepository repository,IPhotoService pService, IMapper mapper)
    {
        _photoService = pService;
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberResponse>>> GetAllAsync()
    {
        var users = await _repository.GetMembersAsync();

        

        return Ok(users);
    }
    [HttpGet("{username}",Name = "GetByUsernameAsync")] // api/users/Calamardo
    public async Task<ActionResult<MemberResponse>> GetByUsernameAsync(string username)
    {
        var member = await _repository.GetMemberAsync(username);

        if (member == null)
        {
            return NotFound();
        }

        return member;
    }
    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateRequest request)
    {
        
        var user = await _repository.GetByUsernameAsync(User.GetUserName());
        if (user == null)
        {
            return BadRequest("Could not find user");
        }
        _mapper.Map(request, user);
        _repository.Update(user);
        if (await _repository.SaveAllAsync())
        {
            return NoContent();
        }
        return BadRequest("Update user failed!");
    }
    [HttpPost("photo")]
    public async Task<ActionResult<PhotoResponse>> AddPhoto(IFormFile file)
    {
        var user = await _repository.GetByUsernameAsync(User.GetUserName());
        if(user == null){
            return BadRequest("Cannot update user");
        }
        var result = await _photoService.AddPhotoAsync(file);
        if(result.Error != null)
        {
            return BadRequest(result.Error.Message);
        
        }
        var photo = new Photo{
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };
        if(await _repository.SaveAllAsync()
        )
        {
            return CreatedAtAction(nameof(GetByUsernameAsync),new {user = user.UserName},_mapper.Map<PhotoResponse>(photo));
        }
        return BadRequest("Problem adding the photo");
    }
}
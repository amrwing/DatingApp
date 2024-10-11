namespace API.DTOs;

using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
    [Required]
    public required string Username{get;set;} = string.Empty;
    [Required]
    [StringLength(8,MinimumLength =  4)]
    public required string Password{get;set;} = string.Empty;
}
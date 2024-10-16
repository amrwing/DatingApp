namespace API.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("photos")]
public class Photo
{
    public int Id {get;set;}
    public required string Url {get;set;}
    public bool IsMain {get;set;}
    public string? PublicId {get;set;}
    //EF Navigation properties
    //Required one-to-many relation
    public int AppUserId {get;set;}
    public AppUser AppUser {get;set;} = null!;

}
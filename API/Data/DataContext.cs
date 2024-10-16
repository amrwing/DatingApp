namespace API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

//PATTERNS USED
//Unit of Work
//Repository
public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }
}

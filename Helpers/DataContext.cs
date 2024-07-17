using KitapService.Models;
using Microsoft.EntityFrameworkCore;

namespace KitapService.Helpers;
public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // in memory database used for simplicity, change to a real db for production applications
        options.UseSqlServer(Configuration.GetConnectionString("KitapCon"));
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Language> Languages { get; set; }
}
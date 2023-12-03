using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class ToDoDBContext : DbContext
{
    public DbSet<ToDoTask> Tasks { get; set; }
    private IConfiguration _configuration;


    public ToDoDBContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MSSQLConnection"));
        //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ToDoTest;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoTask>().Property(s => s.Title).HasMaxLength(100);
    }
}
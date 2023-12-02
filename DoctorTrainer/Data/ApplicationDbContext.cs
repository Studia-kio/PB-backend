using DoctorTrainer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DoctorTrainer.Data;

public class ApplicationDbContext : DbContext
{
    
    public DbSet<User>? Users { get; set; }
    public DbSet<UserToken>? UserTokens { get; set; }
    //public DbSet<ImageData>? ImagesData { get; set; }
    
    public ApplicationDbContext() : base()
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<User>();
        // .HasMany(u => u.BookmarkedStops)
        // .WithOne(b => b.User);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=DoctorTrainer.db");
    }
}
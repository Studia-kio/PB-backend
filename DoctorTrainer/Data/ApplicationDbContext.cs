using DoctorTrainer.DTO;
using DoctorTrainer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DoctorTrainer.Data;

public class ApplicationDbContext : DbContext
{
    
    public DbSet<User>? Users { get; set; }
    public DbSet<UserToken>? UserTokens { get; set; }
    public DbSet<ImageData>? ImagesData { get; set; }
    public DbSet<Circle>? Circles { get; set; }
    
    public ApplicationDbContext() : base()
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=DoctorTrainer.db");
    }
}
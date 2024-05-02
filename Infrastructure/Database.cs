using Infrastructure.Dto;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class Database : DbContext
{
    public Database(DbContextOptions<Database> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientDto>().HasMany(x => x.Founders).WithOne(x => x.Client);
        modelBuilder.Entity<ClientDto>().HasKey(x => x.Id);
        modelBuilder.Entity<FounderDto>().HasKey(x => x.Id);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<ClientDto> Clients { get; set; }
    public DbSet<FounderDto> Founders { get; set; }
}
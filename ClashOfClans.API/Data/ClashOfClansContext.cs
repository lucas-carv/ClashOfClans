using ClashOfClans.API.Core;
using ClashOfClans.API.Model;
using ClashOfClans.API.Model.Guerras;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Data;

public class ClashOfClansContext(DbContextOptions<ClashOfClansContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Clan> Clans { get; set; }
    public DbSet<Guerra> Guerras { get; set; }
    public async Task<bool> Commit()
    {
        var result = await base.SaveChangesAsync();
        return result > 0;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClashOfClansContext).Assembly);
        //modelBuilder.SetDefaultLengths();
    }
}

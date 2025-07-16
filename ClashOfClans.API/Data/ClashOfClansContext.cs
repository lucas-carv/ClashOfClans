using ClashOfClans.API.Core;
using ClashOfClans.API.Model;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Data;

public class ClashOfClansContext : DbContext, IUnitOfWork
{
    public ClashOfClansContext(DbContextOptions<ClashOfClansContext> options)
        : base(options)
    {
    }
    public DbSet<Clan> Clans { get; set; }
    public async Task<bool> Commit()
    {
        return await base.SaveChangesAsync() > 0;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClashOfClansContext).Assembly);
        //modelBuilder.SetDefaultLengths();
    }
}

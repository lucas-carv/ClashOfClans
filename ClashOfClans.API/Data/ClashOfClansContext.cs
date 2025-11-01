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
        modelBuilder.SetDefaultLengths();
    }
}
public static class ModelBuilderExtension
{
    public static void SetDefaultLengths(this ModelBuilder modelBuilder, int stringsMaxLength = 100, string decimalPrecision = "10,4", int datePrecision = 0)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
        {
            int? maxLength = property.GetMaxLength();

            if (maxLength is null || maxLength == 0)
                maxLength = stringsMaxLength;

            property.SetColumnType($"varchar({maxLength})");
        }

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(Decimal))))
        {
            property.SetColumnType($"decimal({decimalPrecision})");
        }

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(DateTime))))
        {
            property.SetPrecision(datePrecision);
        }
    }
}
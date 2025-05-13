using ClashOfClans.Integracao.API.Core;
using ClashOfClans.Integracao.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClashOfClans.Integracao.API.Data;

public class ClashOfClansContext : DbContext, IUnitOfWork
{
    public ClashOfClansContext(DbContextOptions<ClashOfClansContext> options)
        : base(options)
    {
    }

    public DbSet<Historico> Historico { get; set; }
    public async Task<bool> Commit()
    {
        return await base.SaveChangesAsync() > 0;
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
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var stringProperties = entityType.GetProperties()
                                             .Where(p => p.ClrType == typeof(string) && (p.GetMaxLength() is null || p.GetMaxLength() == 0));

            foreach (var property in stringProperties)
            {
                modelBuilder.Entity(entityType.ClrType)
                            .Property(property.Name)
                            .HasMaxLength(stringsMaxLength)
                            .HasColumnType($"varchar({stringsMaxLength})");
            }

            // Configura propriedades decimais (caso tenha alguma lógica futura para isso)
            var decimalProperties = entityType.GetProperties()
                                              .Where(p => p.ClrType == typeof(decimal));

            foreach (var property in decimalProperties)
            {
                modelBuilder.Entity(entityType.ClrType)
                            .Property(property.Name)
                            .HasColumnType($"decimal({decimalPrecision})");
            }
        }
    }
}

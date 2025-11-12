using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Core.Extensions;

public static class ChangeTrackerExtension
{
    /// <summary>
    /// Habilita exclusão de registros com soft delete, quando chamado dentro do método de Commit do DBContext
    /// </summary>
    public static void EnableSoftDelete(this ChangeTracker changeTracker)
    {
        foreach (var entry in changeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataAlteracao") != null).ToList())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("DataCriacao").CurrentValue = DateTime.Now;
                entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("DataCriacao").IsModified = false;
                entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
            }
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Unchanged;
                entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                entry.Property("FoiRemovido").CurrentValue = null;
            }
        }
    }
}

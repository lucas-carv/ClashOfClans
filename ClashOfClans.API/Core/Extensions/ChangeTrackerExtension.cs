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
        var agora = HorarioBrasil.Agora;

        foreach (var entry in changeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataAlteracao") != null).ToList())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("DataCriacao").CurrentValue = agora;
                entry.Property("DataAlteracao").CurrentValue = agora;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("DataCriacao").IsModified = false;
                entry.Property("DataAlteracao").CurrentValue = agora;
            }
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Unchanged;
                entry.Property("DataAlteracao").CurrentValue = agora;
                entry.Property("FoiRemovido").CurrentValue = null;
            }
        }
    }
}

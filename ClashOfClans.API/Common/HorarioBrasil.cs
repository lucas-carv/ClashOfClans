using System.Runtime.InteropServices;

namespace ClashOfClans.API.Common;

public static class HorarioBrasil
{
    private static readonly TimeZoneInfo FusoBrasil = GetTimeZone();

    private static TimeZoneInfo GetTimeZone()
    {
        // Windows usa um ID, Linux (Render) usa outro
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        }

        // Linux / Docker / Render
        return TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
    }

    /// <summary>
    /// Retorna o DateTime no horário de Brasília (UTC-3, considerando horário de verão histórico).
    /// </summary>
    public static DateTime Agora =>
        TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, FusoBrasil);
}
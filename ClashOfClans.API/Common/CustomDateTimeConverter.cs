using Newtonsoft.Json;
using System.Globalization;
using System.Runtime.InteropServices;

namespace ClashOfClans.API.Common;
public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String)
        {
            var s = (string)reader.Value;
            // tenta parsear no formato esperado
            if (DateTime.TryParseExact(
                s,
                "yyyyMMdd'T'HHmmss.fff'Z'",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal, // pega UTC
                out var dtUtc))
            {
                TimeZoneInfo FusoBrasil = GetTimeZone();

                var dataConvertida = TimeZoneInfo.ConvertTimeFromUtc(dtUtc, FusoBrasil);
                return dataConvertida;
            }
        }
        return DateTime.MinValue;
    }
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
    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        // escreve no mesmo formato
        writer.WriteValue(value.ToUniversalTime().ToString("yyyyMMdd'T'HHmmss.fff'Z'"));
    }
}
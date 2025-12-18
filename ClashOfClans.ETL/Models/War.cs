using Newtonsoft.Json;
using System.Globalization;

namespace ClashOfClans.ETL.Models;

public record War
{
    public StatusGuerra State { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime StartTime { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime EndTime { get; set; }
    public ClanWar Clan { get; set; } = new();
}
public record ClanWar
{
    public string Tag { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<MembersWarDTO> Members { get; set; } = [];
}

public record MembersWarDTO
{
    public string Tag { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<AttacksDTO> Attacks { get; set; } = [];

}
public record AttacksDTO
{
    public required string AttackerTag { get; set; }
    public required string DefenderTag { get; set; }
    public int Stars { get; set; }
}
public enum StatusGuerra
{
    NotInWar,
    WarEnded,
    Preparation,
    InWar
}

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
                var dtLocal = dtUtc.ToLocalTime(); // converte para o fuso da máquina
                return dtLocal;
            }
        }
        return DateTime.MinValue;
    }

    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        // escreve no mesmo formato
        writer.WriteValue(value.ToUniversalTime().ToString("yyyyMMdd'T'HHmmss.fff'Z'"));
    }
}
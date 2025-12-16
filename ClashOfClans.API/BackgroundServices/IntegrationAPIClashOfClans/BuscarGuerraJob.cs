using ClashOfClans.API.Application.Commands.Guerras;
using ClashOfClans.API.Data;
using ClashOfClans.API.DTOs.Guerras;
using MediatR;
using Newtonsoft.Json;
using Quartz;
using System.Globalization;
using System.Runtime.InteropServices;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans;

public class BuscarGuerraJob(ClashOfClansService clashOfClansService, ClashOfClansContext context, IMediator mediator) : IJob
{
    private readonly ClashOfClansService _clashOfClansService = clashOfClansService;
    private readonly ClashOfClansContext _context = context;

    private readonly IMediator _mediator = mediator;
    public async Task Execute(IJobExecutionContext context)
    {
        string tag = "#2L0UC9R8P";
        string encodedTag = Uri.EscapeDataString(tag);

        War war = await _clashOfClansService.BuscarGuerra(encodedTag);
        if (war is null)
        {
            Console.WriteLine($"Guerra não encontrada na API");
            return;
        }
        if (war.State.Equals(StatusGuerra.NotInWar))
            return;

        ClanEmGuerraDTO clan = new()
        {
            ClanLevel = 0,
            Nome = war.Clan.Name,
            Tag = war.Clan.Tag,
            Membros = war.Clan.Members.Select(m => new MembroEmGuerraDTO()
            {
                CentroVilaLevel = 0,
                Nome = m.Name,
                Tag = m.Tag,
                Ataques = m.Attacks.Select(a => new AtaquesDTO()
                {
                    Estrelas = a.Stars,
                    AtacanteTag = a.AttackerTag,
                    DefensorTag = a.DefenderTag
                })
            })
        };

        UpsertGuerraRequest upsertGuerraRequest = new(war.State.ToString(), war.StartTime, war.EndTime, "Normal", clan);

        await _mediator.Send(upsertGuerraRequest);
    }
}

public class War
{
    public StatusGuerra State { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime StartTime { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime EndTime { get; set; }
    public ClanWar Clan { get; set; } = new();
}

public enum StatusGuerra
{
    NotInWar,
    WarEnded,
    Preparation,
    InWar
}

public class ClanWar
{
    public string Tag { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<MembersWarDTO> Members { get; set; } = [];
}

public class MembersWarDTO
{
    public string Tag { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<AttacksDTO> Attacks { get; set; } = [];

}
public class AttacksDTO
{
    public string AttackerTag { get; set; }
    public string DefenderTag { get; set; }
    public int Stars { get; set; }

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
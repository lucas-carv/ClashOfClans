using System.Globalization;
using System.Runtime.InteropServices;
using MediatR;
using Quartz;
using Newtonsoft.Json;
using ClashOfClans.API.Application.Commands.Guerras;
using ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Responses;
using ClashOfClans.API.DTOs.Guerras;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans;

public class BuscarGuerraJob(ClashOfClansService clashOfClansService, IMediator mediator) : IJob
{
    private readonly ClashOfClansService _clashOfClansService = clashOfClansService;

    private readonly IMediator _mediator = mediator;
    public async Task Execute(IJobExecutionContext context)
    {
        string tag = "#2L0UC9R8P";
        string encodedTag = Uri.EscapeDataString(tag);

        WarResponse war = await _clashOfClansService.BuscarGuerra(encodedTag);
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
using ClashOfClans.API.BackgroundServices;
using ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans;
using ClashOfClans.API.Data;
using ClashOfClans.API.Services.Guerras;
using Microsoft.EntityFrameworkCore;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

//var assembly = AppDomain.CurrentDomain.Load("ClashOfClans.API");
//AssemblyScanner.FindValidatorsInAssembly(assembly).ForEach(result => builder.Services.AddScoped(result.InterfaceType, result.ValidatorType));
//builder.Services.AddMediatR(assembly);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Informe a versão explicitamente, em vez de usar AutoDetect:
var serverVersion = new MariaDbServerVersion(new Version(10, 5, 29));


builder.Services.AddDbContext<ClashOfClansContext>(options =>
{
    options.UseMySql(connectionString, serverVersion, mySqlOptions =>
    {
        mySqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null);
    })
        .UseSnakeCaseNamingConvention();
});

builder.Services.AddScoped<GuerraService>();
builder.Services.AddScoped<ClashOfClansService>();
builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("AnalisarGuerrasJob");
    var jobKey2 = new JobKey("DetectarMembrosInativosEmGuerrasJob");
    var jobKey3 = new JobKey("BuscarClanJob");
    q.AddJob<AnalisarGuerrasJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(t => t
         .ForJob(jobKey)
         .WithIdentity("AnalisarGuerrasTrigger")
         .StartNow() // executa logo na inicialização
         .WithSimpleSchedule(s => s
             .WithIntervalInMinutes(5)
             .RepeatForever())); // repete para sempre

    q.AddJob<DetectarMembrosInativosEmGuerrasJob>(opts => opts.WithIdentity(jobKey2));

    q.AddTrigger(t => t
         .ForJob(jobKey2)
         .WithIdentity("DetectarMembrosInativosEmGuerrasTrigger")
         .StartNow() // executa logo na inicialização
         .WithSimpleSchedule(s => s
             .WithIntervalInMinutes(5)
             .RepeatForever())); // repete para sempre

    q.AddJob<BuscarClanJob>(opts => opts.WithIdentity(jobKey3));

    q.AddTrigger(t => t
         .ForJob(jobKey3)
         .WithIdentity("BuscarClanTrigger")
         .StartNow() // executa logo na inicialização
         .WithSimpleSchedule(s => s
             .WithIntervalInMinutes(5)
             .RepeatForever())); // repete para sempre
});

builder.Services.AddQuartzHostedService(o =>
{
    o.WaitForJobsToComplete = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();


if (app.Environment.IsProduction())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ClashOfClansContext>();
    db.Database.Migrate(); // cria/atualiza o schema no Johnny
}

app.MapControllers();

app.Run();

using ClashOfClans.API.Core;
using ClashOfClans.API.Data;
using ClashOfClans.API.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
builder.Services.AddScoped<IClanRepository, ClanRepository>();

var assembly = AppDomain.CurrentDomain.Load("ClashOfClans.API");
AssemblyScanner.FindValidatorsInAssembly(assembly).ForEach(result => builder.Services.AddScoped(result.InterfaceType, result.ValidatorType));
builder.Services.AddMediatR(assembly);

builder.Services.AddDbContext<ClashOfClansContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ).UseSnakeCaseNamingConvention());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

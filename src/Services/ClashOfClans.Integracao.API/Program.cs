using ClashOfClans.Integracao.API.Configurations;
using ClashOfClans.Integracao.API.Core;
using ClashOfClans.Integracao.API.Data;
using ClashOfClans.Integracao.API.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
builder.Services.AddScoped<IGuerrasRepository, GuerrasRepository>();

var assembly = AppDomain.CurrentDomain.Load("ClashOfClans.Integracao.API");
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

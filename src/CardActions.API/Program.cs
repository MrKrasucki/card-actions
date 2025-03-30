using CardActions.API.Models.Api;
using CardActions.API.Persistence;
using CardActions.API.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("card-actions-db"));

builder.Services.AddScoped<IValidator<GetActionsRequest>, GetActionsRequestValidator>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICardService, CardService>();
builder.Services.AddScoped<IActionsService, ActionsService>();

var app = builder.Build();

Seed.SeedDatabase(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapActionEndpoints();

app.Run();

// needed for integration testing
public partial class Program {}
using BetApplication.Interfaces;
using BetApplication.Repositories;
using BetFacade;
using BetPersistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBetRepository, BetRepository>();
builder.Services.AddScoped<ISelectionRepository, ISelectionRepository>();
builder.Services.AddScoped<IBetFacade, BetFacade.BetFacade>();
builder.Services.AddDbContext<BetContext>(opt =>
{
    opt.UseSqlServer("Server=.\\SQLEXPRESS; Database=rasbet_bet; Uid=rasbet; Pwd=Pa$$w0rd");
});
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

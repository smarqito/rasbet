using GameOddAPI;
using GameOddApplication.Interfaces;
using GameOddApplication.Repositories;
using GameOddPersistance;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GameOddContext>(opt =>
{
    opt.UseSqlServer("Server=.\\SQLEXPRESS; Database=rasbet_gameOdd; Uid=rasbet; Pwd=Abc123");
});
//builder.Services.AddScoped<GameOddContext>();
builder.Services.AddScoped<IGameOddFacade, GameOddFacade>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IBetTypeRepository, BetTypeRepository>();
builder.Services.AddScoped<ISportRepository, SportRepository>();
var app = builder.Build();

UpdateGames updateGames = new UpdateGames(app.Services.GetRequiredService<GameOddFacade>());
Thread thr = new Thread(new ThreadStart(updateGames.Thread1));
thr.Start();

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

using GameOddAPI;
using GameOddApplication;
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
builder.Services.AddAutoMapper(typeof(ApplicationServicesProfile));

builder.Services.AddDbContext<GameOddContext>(opt =>
{
    opt.UseSqlServer("Server=.\\SQLEXPRESS; Database=rasbet_gameOdd; Uid=rasbet; Pwd=Pa$$w0rd");
    opt.UseLazyLoadingProxies();
    opt.EnableSensitiveDataLogging(true);
});
builder.Services.AddScoped<IGameOddFacade, GameOddFacade>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IBetTypeRepository, BetTypeRepository>();
builder.Services.AddScoped<ISportRepository, SportRepository>();


var app = builder.Build();
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
UpdateGames updateGames = new UpdateGames(services.GetRequiredService<IGameOddFacade>());
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

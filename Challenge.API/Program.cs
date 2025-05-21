using Challenge.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;


builder.Services.AddDbContext<ChallengeDBContext>(options =>
    options.UseNpgsql("Server=localhost;Port=5432;Database=CHALLENGEDB;userId=postgres;Password=1;"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
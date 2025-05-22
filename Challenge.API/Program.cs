using Challenge.API;
using Challenge.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;


builder.Services.AddDbContext<ChallengeDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnectionString")));
builder.Services.GetServiceCollection();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});




var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Challenge API V1");
    c.RoutePrefix = string.Empty;
});
app.MapControllers();
app.UseCors("AllowAll");
app.Run();
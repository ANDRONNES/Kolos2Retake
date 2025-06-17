using Kol2.DAL;
using kol2.Middlewares;
using Kol2.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string? connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    opt.UseSqlServer(connectionString);
});

var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();
app.Run();


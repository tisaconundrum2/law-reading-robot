using LawRobot.Core.Services;
using LawRobot.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("LawRobot")
    ?? builder.Configuration["ConnectionStrings:LawRobot"]
    ?? "Host=localhost;Database=postgres;Username=postgres;Password=postgres";

builder.Services.AddDbContext<LawRobotDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddSingleton<IContentHashService, HmacSha256ContentHashService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

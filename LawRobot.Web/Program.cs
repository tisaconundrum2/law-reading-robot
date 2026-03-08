using LawRobot.Core.Services;
using LawRobot.Data;
using LawRobot.Web.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication("AdminApiKey")
    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("AdminApiKey", _ => { });
builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("LawRobot")
    ?? throw new InvalidOperationException("ConnectionStrings:LawRobot must be configured.");

builder.Services.AddDbContext<LawRobotDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddSingleton<IContentHashService, HmacSha256ContentHashService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

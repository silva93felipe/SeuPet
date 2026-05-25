using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SeuPet.Domain.Context;
using SeuPet.Domain.Services;
using SeuPet.Infra;
using SeuPet.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices();
builder.Services.AddDbContext<SeuPetContext>( options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionDb"));
    //options.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionDb"));
});
builder.Services.AddStackExchangeRedisCache(c => {
    c.Configuration = builder.Configuration.GetConnectionString("ConnectionCache");
    //c.Configuration = Environment.GetEnvironmentVariable("ConnectionCache");
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    TokenJwtService.GetTokenValidationParameters(builder.Configuration);
});

builder.Services.AddAuthorization(); 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
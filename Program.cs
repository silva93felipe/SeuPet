using Microsoft.EntityFrameworkCore;
using SeuPet.Api.Context;
using SeuPet.Infra;

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
app.UseAuthorization();
app.MapControllers();
app.Run();
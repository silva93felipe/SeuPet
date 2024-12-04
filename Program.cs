using Microsoft.EntityFrameworkCore;
using SeuPet.Repository;
using SeuPet.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IAdotanteService, AdotanteService>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IAdotanteRepository, AdotanteRepository>();
builder.Services.AddDbContext<SeuPetContext>( options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("Test"));
});
builder.Services.AddStackExchangeRedisCache(c => {
    c.Configuration = "localhost:6379";
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
app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();
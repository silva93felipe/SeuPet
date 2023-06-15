using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Domain.Interfaces.Services;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Services;
using Infra.Repositories;
using Infra.Context;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conexaoDb = builder.Configuration.GetConnectionString("Test");
builder.Services.AddControllers();
builder.Services.AddDbContext<PetContext>(opt => opt.UseNpgsql(conexaoDb));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Repositories
builder.Services.AddScoped<IDonoRepository, DonoRepository>();
#endregion

#region Services
builder.Services.AddScoped<IDonoService, DonoService>();
#endregion



var app = builder.Build();

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

using BeamServer.Entities;
using BeamServer.Models;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);



// Add EF Core
builder.Services.AddDbContext<BeamDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), opt =>
    {
        opt.MigrationsAssembly("BeamServer");
    }));

// Add services to the container.
builder.Services
    .AddIdentityApiEndpoints<BeamUser>()
    .AddEntityFrameworkStores<BeamDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Constants.ServerId = builder.Configuration["ServerId"];

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BeamDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGroup("api/account").MapIdentityApi<BeamUser>();

app.UseFileServer();

app.Run();

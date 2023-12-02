using Beam.Client;
using Beam.Extensions;
using BeamServer.Entities;
using BeamServer.Models;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// beam configuration
builder.Services.AddApi(options =>
{
    // the type of token here depends on the api security specifications
    var token = new ApiKeyToken(builder.Configuration["ApiKey"]);
    options.AddTokens(token);

    // optionally choose the method the tokens will be provided with, default is RateLimitProvider
    options.UseProvider<RateLimitProvider<ApiKeyToken>, ApiKeyToken>();

    options.ConfigureJsonOptions((jsonOptions) =>
    {
        // your custom converters if any
    });

    options.AddApiHttpClients(
        client: client =>
        {
            client.BaseAddress = new Uri("https://api.testnet.onbeam.com");
        },
        builder: builder => {
            builder
            .AddRetryPolicy(2)
            .AddTimeoutPolicy(TimeSpan.FromSeconds(20))
            .AddCircuitBreakerPolicy(10, TimeSpan.FromSeconds(30));
            // add whatever middleware you prefer
        }
    );
});

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
builder.Services.AddMemoryCache();

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

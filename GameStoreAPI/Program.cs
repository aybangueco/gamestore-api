using GameStoreAPI.Common;
using GameStoreAPI.Common.Interfaces;
using GameStoreAPI.Endpoints;
using GameStoreAPI.Models;
using GameStoreAPI.Repository;
using GameStoreAPI.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddOpenApi();
}

builder.Services.AddDbContext<ModelsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddValidation();

builder.Services.AddAuthentication()
    .AddJwtBearer("some-scheme", jwtOptions =>
    {
        jwtOptions.MetadataAddress = builder.Configuration["Api:MetadataAddress"];
        // Optional if the MetadataAddress is specified
        jwtOptions.Authority = builder.Configuration["Api:Authority"];
        jwtOptions.Audience = builder.Configuration["Api:Audience"];
        jwtOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidAudiences = builder.Configuration.GetSection("Api:ValidAudiences").Get<string[]>(),
            ValidIssuers = builder.Configuration.GetSection("Api:ValidIssuers").Get<string[]>()
        };

        jwtOptions.MapInboundClaims = false;
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp
    => exceptionHandlerApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        
        var exception = context.Features.Get<IExceptionHandlerFeature>();
        if (exception != null)
        {
            var error = exception.Error;
            
            await context.Response.WriteAsJsonAsync(new
            {
                statusCode = context.Response.StatusCode,
                message = error.Message
            });           
        }
    }));

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ModelsContext>();
    await Seed.PopulateGenresAsync(dbContext);
}

app.MapGamesEndpoint();
app.MapGenresEndpoint();
app.MapAuthEndpoints();
app.Run();
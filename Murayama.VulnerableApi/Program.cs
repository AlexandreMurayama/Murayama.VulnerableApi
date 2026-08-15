using Microsoft.EntityFrameworkCore;
using Murayama.VulnerableApi.Data;
using Murayama.VulnerableApi.Data.Seed;
using Murayama.VulnerableApi.Services;
using Murayama.VulnerableApi.Settings;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Murayama.VulnerableApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<PasswordService>();
        builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection(JwtSettings.SectionName));

        builder.Services.AddScoped<JwtService>();

        var jwtSettings = builder.Configuration
                              .GetSection(JwtSettings.SectionName)
                              .Get<JwtSettings>()
                          ?? throw new InvalidOperationException("JWT settings are not configured.");

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Key)),

                    ClockSkew = TimeSpan.Zero
                };
            });

        builder.Services.AddAuthorization();
        
        var app = builder.Build();
        
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var passwordService = scope.ServiceProvider.GetRequiredService<PasswordService>();

            await dbContext.Database.MigrateAsync();
            await DatabaseSeeder.SeedAsync(dbContext, passwordService);
        }
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
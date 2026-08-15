using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Murayama.VulnerableApi.Data;
using Murayama.VulnerableApi.DTOs.Auth;
using Murayama.VulnerableApi.Services;
using Murayama.VulnerableApi.Settings;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly PasswordService _passwordService;
    private readonly JwtService _jwtService;
    private readonly JwtSettings _jwtSettings;

    public AuthController(
        AppDbContext dbContext,
        PasswordService passwordService,
        JwtService jwtService,
        IOptions<JwtSettings> jwtOptions)
    {
        _dbContext = dbContext;
        _passwordService = passwordService;
        _jwtService = jwtService;
        _jwtSettings = jwtOptions.Value;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(
        LoginRequest request)
    {
        var user = await _dbContext.Users
            .SingleOrDefaultAsync(u => u.Email == request.Email);

        if (user is null ||
            !_passwordService.VerifyPassword(
                user,
                user.PasswordHash,
                request.Password))
        {
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
        }

        var token = _jwtService.GenerateToken(user);

        return Ok(new LoginResponse
        {
            AccessToken = token,
            ExpiresIn = _jwtSettings.ExpirationMinutes * 60
        });
    }
}
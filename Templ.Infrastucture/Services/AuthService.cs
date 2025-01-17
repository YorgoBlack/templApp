﻿using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Templ.Infrastucture.Services;

public class AuthService : IAuthService
{
    private readonly IAppConfiguration _configuration;

    public AuthService(IAppConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Token(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.JwtSecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", username) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public (string?,string?) RefreshToken(string token)
    {
        string? user = null;
        string? reToken = null;

        var securityToken = TryGetSecurityToken(token);
        user = securityToken?.Claims.First(x => x.Type == "id").Value;
        if( user != null )
            reToken = securityToken?.ValidTo < DateTime.UtcNow ? token : Token(user);

        return (user,reToken);
    }

    public string? TryGetTokenUser(string token)
    {
        var securityToken = TryGetSecurityToken(token);
        if(securityToken != null)
        {
            return securityToken.Claims.First(x => x.Type == "id").Value;
        }
        return null;
    }

    private JwtSecurityToken? TryGetSecurityToken(string token)
    {
        if (token == null) return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.JwtSecretKey);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            
            return (JwtSecurityToken) validatedToken;
        }
        catch
        {
            return null;
        }
    }
}
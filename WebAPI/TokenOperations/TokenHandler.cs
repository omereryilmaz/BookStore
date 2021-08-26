using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Entities;
using WebAPI.TokenOperations.Models;

namespace WebAPI.TokenOperations
{
  public class TokenHandler
  {
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public Token CreateAccessToken(User user)
    {
      Token tokenModel = new Token();

      SymmetricSecurityKey key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"])
      );

      // Sign Credentials usign key with HmacSha256 security algorithm
      SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      tokenModel.Expiration = DateTime.Now.AddMinutes(15);

      // Create security token
      JwtSecurityToken securityToken = new JwtSecurityToken(
        issuer: _configuration["Token:Issuer"],
        audience: _configuration["Token:Audience"],
        expires: tokenModel.Expiration,
        notBefore: DateTime.Now,
        signingCredentials: credentials
      );
      
      JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

      // Token creating
      tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
      tokenModel.RefreshToken = CreateRefreshToken();

      return tokenModel;
    }

    public string CreateRefreshToken()
    {
      return Guid.NewGuid().ToString();
    }
  }
}
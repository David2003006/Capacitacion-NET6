using Microsoft.AspNetCore.Mvc;
using TestBankApi.Data.DTOs;
using BankApi.Data.BankModels;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace BankApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class LoginController: ControllerBase
{

    private readonly LoginServices _loginServices;

    private IConfiguration config;

    public LoginController(LoginServices loginServices, IConfiguration config)
    {
        _loginServices= loginServices;
        this.config= config;
    }

    [HttpPost("autenticacion")]
    public async Task<IActionResult> Login(ClientLoginDto clientLogin)
    {
    var admin = await _loginServices.GetLogin(clientLogin);

    if (admin is null)
        return BadRequest(new {message= "Invalid credentials"}); // Returning a meaningful message

    string JwtToken= GenerateToken(admin);

    return Ok(new {message=JwtToken});
    }  

    private string GenerateToken(User user)
    {
        var claims = new []
        {
           new  Claim( ClaimTypes.Name, user.Name),
           new Claim(ClaimValueTypes.Email, user.Email)
        };

        var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:key").Value));
        var creds= new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds);

        string token= new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }

}
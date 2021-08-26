using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DBOperations;
using WebAPI.Application.UserOperations.Commands.CreateUser;
using Microsoft.Extensions.Configuration;
using WebAPI.TokenOperations.Models;
using WebAPI.Application.UserOperations.Commands.CreateToken;
using WebAPI.Application.UserOperations.Commands.RefreshToken;

namespace WebAPI.Controllers
{
  [ApiController]
  [Route("[controller]s")]
  public class UserController : ControllerBase
  {
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
    {
      _context = context;
      _mapper = mapper;
      _configuration = configuration;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateUserModel newUser)
    {
      CreateUserCommand command = new CreateUserCommand(_context, _mapper);
      command.Model = newUser;
      command.Handle();

      return Ok();
    }

    [HttpPost("connect/token")]
    public ActionResult<Token> RefreshToken([FromBody] CreateTokenModel login)
    {
      CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
      command.Model = login;
      var token = command.Handle();

      return token;
    }

    [HttpGet("refresh-token")]    
    public ActionResult<Token> CreateToken([FromQuery] string token)
    {
      RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
      command.RefreshToken = token;
      var result = command.Handle();

      return result;
    }
  }
}
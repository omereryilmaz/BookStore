using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebAPI.DBOperations;
using WebAPI.Entities;

namespace WebAPI.Application.UserOperations.Commands.CreateToken
{
  public class CreateTokenCommand
  {
    public CreateTokenModel Model { get; set; }
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    public CreateTokenCommand(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
    {
      _context = context;
      _mapper = mapper;
      _configuration = configuration;
    }

    public void Handle()
    {
      var user = _context.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
      if(user is not null)
      {
        // Create token
        
      }
      else
      {
        throw new InvalidOperationException("Kullanici adi veya sifre hatali!");
      }
    }

    public class CreateTokenModel
    {
      public string Email { get; set; }
      public string Password { get; set; }
    }
  }
}

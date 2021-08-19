using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Middlewares
{
  public class CustomExceptionMiddleware
  { 
    private readonly RequestDelegate _next;
    public CustomExceptionMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      string message = "[Request] HTTP " + context.Request.Method +
        " - " + context.Request.Path;
      Console.WriteLine(message);
      await _next(context); // bir sonraki middleware calistir
    }
  }

  public static class CustomExceptionMiddlewareExtension
  {
    public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
  }
}
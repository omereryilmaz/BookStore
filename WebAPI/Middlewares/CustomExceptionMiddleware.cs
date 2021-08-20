using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
      var watch = Stopwatch.StartNew();

      try
      {
        string message = "[Request]  HTTP " + context.Request.Method +
          " - " + context.Request.Path;
        Console.WriteLine(message);
        await _next(context); // bir sonraki middleware calistir

        watch.Stop();
        message = "[Response] HTTP " + context.Request.Method +
          " - " + context.Request.Path + " responded " +
          context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
        Console.WriteLine(message);
      }
      catch (Exception ex)
      {
        watch.Stop();
        await HandlException(context, ex, watch);
      }
    }
    private Task HandlException(HttpContext context, Exception ex, Stopwatch watch)
    {
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

      string message = "[Error]   HTTP " + context.Request.Method +
        " - " + context.Response.StatusCode + " Error Message: " +
        ex.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms";
      Console.WriteLine(message);

      var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
      return context.Response.WriteAsync(result);
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
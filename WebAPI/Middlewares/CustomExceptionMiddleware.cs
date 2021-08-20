using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebAPI.Services;

namespace WebAPI.Middlewares
{
  public class CustomExceptionMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILoggerService _loggerService;
    public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
    {
      _next = next;
      _loggerService = loggerService;
    }

    public async Task Invoke(HttpContext context)
    {
      // Start timer to calculate response time
      var watch = Stopwatch.StartNew();

      try
      {
        // Create log message for requests
        string message = "[Request]  HTTP " + context.Request.Method +
          " - " + context.Request.Path;

        // Write log message using logger service
        _loggerService.Write(message);
        
        // Run next middleware
        await _next(context);

        // Stop timer
        watch.Stop();
        // Create log message for responses
        message = "[Response] HTTP " + context.Request.Method +
          " - " + context.Request.Path + " responded " +
          context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
        // Write log message using logger service
        _loggerService.Write(message);
      }
      catch (Exception ex)
      {
        // Stop timer
        watch.Stop();
        await HandlException(context, ex, watch);
      }
    }
    private Task HandlException(HttpContext context, Exception ex, Stopwatch watch)
    {
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
      
      // Create log message for errors
      string message = "[Error]   HTTP " + context.Request.Method +
        " - " + context.Response.StatusCode + " Error Message: " +
        ex.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms";
      
      // Write log message using logger service
      _loggerService.Write(message);
      
      // Convert object to JSON
      var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
      return context.Response.WriteAsync(result);
    }
  }

  public static class CustomExceptionMiddlewareExtension
  {
    // Adding custom middleware
    public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
  }
}
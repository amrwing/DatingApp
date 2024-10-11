namespace API.Middlewares;

using System.Net;
using System.Text.Json;
using API.Errors;

public class ExceptionMiddleware(RequestDelegate next, 
ILogger<ExceptionMiddleware> logger, 
IHostEnvironment env)
{
    private readonly JsonSerializerOptions serializer = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
    public async Task InvokeAsync(HttpContext context){
        try{
            await next(context);
        }catch(Exception ex){
            logger.LogError(ex,ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = env.IsDevelopment()
            ? new ApiException(context.Response.StatusCode,ex.Message, ex.StackTrace) : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server error");
            var options = serializer;
            var json = JsonSerializer.Serialize(response,options);
            await context.Response.WriteAsync(json);
        }
    }
}
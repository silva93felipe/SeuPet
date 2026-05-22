using System.Net;
using SeuPet.Api.Dto;

public class GlobalExceptionMiddleware{
    private readonly RequestDelegate _next;
    
    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context){
        try
        {
            await _next(context);
        }
        catch (ApplicationException exception){
            Console.WriteLine(exception.Message);
            var response = new ResponseHttp(HttpStatusCode.BadRequest, false, new List<string>(){ exception.Message});
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            var response = new ResponseHttp(HttpStatusCode.InternalServerError, false, new List<string>(){ "Internal Server Error" });
            context.Response.StatusCode =StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(response);
        }
    }

}
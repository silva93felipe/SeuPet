using System.Net;
using SeuPet.Dto;

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
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            var response = new ResponseHtttp(HttpStatusCode.InternalServerError, false, new List<string>(){ "Server Error" });
            context.Response.StatusCode =StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(response);
        }
    }

}
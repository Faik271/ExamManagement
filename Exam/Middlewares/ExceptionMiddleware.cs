using System.Net;
using System.Text.Json;
using Exam.Models;
using FluentValidation;

namespace ExamManagementApp.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred: {ex.Message}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = (int)HttpStatusCode.InternalServerError;
        var response = new CommonResponse<object>(false, "An unexpected error occurred.", null);

        if (exception is ValidationException || exception is System.ComponentModel.DataAnnotations.ValidationException)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
            response = new CommonResponse<object>(false, exception.Message, null);
        }

        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}

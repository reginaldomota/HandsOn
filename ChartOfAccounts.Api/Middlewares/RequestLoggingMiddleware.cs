using ChartOfAccounts.CrossCutting.Context.Interfaces;
using System.Diagnostics;
using System.Text;

namespace ChartOfAccounts.Api.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IRequestContext requestContext)
    {
        // Captura dados da requisição
        var request = context.Request;
        var requestId = requestContext.RequestId;
        var requestBody = string.Empty;

        // Lê o corpo da requisição, se existir
        if (request.ContentLength > 0 && request.Body.CanRead)
        {
            request.EnableBuffering();
            
            using var reader = new StreamReader(
                request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);
                
            requestBody = await reader.ReadToEndAsync();
            request.Body.Position = 0;
        }

        // Registra início da requisição
        _logger.LogInformation(
            "Requisição {RequestId} iniciada: {Method} {Path} - IP: {IP} - Body: {Body}",
            requestId, request.Method, request.Path, context.Connection.RemoteIpAddress, 
            string.IsNullOrEmpty(requestBody) ? "[empty]" : requestBody);

        // Prepara para capturar a resposta
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro não tratado na requisição {RequestId}", requestId);
            context.Response.Body = originalBodyStream;
            throw;
        }
        finally
        {
            stopwatch.Stop();
            
            // Lê o corpo da resposta
            responseBody.Seek(0, SeekOrigin.Begin);
            var response = await new StreamReader(responseBody).ReadToEndAsync();
            responseBody.Seek(0, SeekOrigin.Begin);

            // Copia para o stream original e finaliza
            await responseBody.CopyToAsync(originalBodyStream);

            // Registra conclusão da requisição
            _logger.LogInformation(
                "Requisição {RequestId} concluída: {StatusCode} em {ElapsedMs}ms - Resposta: {Response}",
                requestId, context.Response.StatusCode, stopwatch.ElapsedMilliseconds,
                string.IsNullOrEmpty(response) ? "[empty]" : response);
        }
    }
}
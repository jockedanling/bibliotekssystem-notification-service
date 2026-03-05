namespace bibliotekssystem_notification_service.Middleware;

/*
 Flödet: HTTP-anrop -> Middleware -> Controller -> Database
 Om GET: går direkt vidare
 Om POST/PUT/DELETE utan nyckel: returnerar 401
 Om POST/PUT/DELETE med rätt nyckel: går vidare
 */

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeaderName = "X-API-Key";
    
    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;   
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Steg 1: Om det är GET -> Släpp igenom direkt

        if (context.Request.Method == HttpMethods.Get)
        {
            await _next(context);
            return;
        }
        
        // Steg 2: Kontrollera att X-Api-Key-headern finns
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new {error = "Api-nyckel saknas. " + "Skicka header: X-Api-Key"});
            return;
        }
        
        //Steg 3: Hämta rätt nyckel från konfiguration
        var configuration = context.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = configuration.GetValue<string>("ApiSettings:ApiKey");
        
        // Steg 4: Jämför nycklarna
        if (string.IsNullOrEmpty(apiKey) || !apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Ogiltig Api-nyckel."
            });
            return;
        }
        
        // Steg 5: Nyckel stämmer -> fortsätt till controllern
        await _next(context);
        
    }
}
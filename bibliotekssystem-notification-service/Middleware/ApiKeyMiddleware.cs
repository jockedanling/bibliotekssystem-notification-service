namespace bibliotekssystem_notification_service.Middleware;

/*
 Middleware som skyddar skrivoperationer med en API-nyckel
 GET-anrop släpps igneom direkt
 POST, PUT och delete kräver en giltig i headern X-API-Key
 Flöde: HTTP-anrop > Middleware > Controller > Databas
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
        
        // Steg 2: Kontrollera att X-Api-Key-headern finns -> annars 401
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new {error = "Api-nyckel saknas. " + "Skicka header: X-Api-Key"});
            return;
        }
        
        //Steg 3: Hämta rätt nyckel från konfiguration
        var configuration = context.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = configuration.GetValue<string>("ApiSettings:ApiKey");
        
        // Steg 4: Jämför nycklarna -> annars 401
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
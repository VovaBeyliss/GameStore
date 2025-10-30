using System.Text;
using System.Text.Json;

namespace GameStore.Middlewares {
    public class HttpContextWriteMiddleware {
        private readonly RequestDelegate _next;

        public HttpContextWriteMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            Console.WriteLine("HttpContextWriteMiddleware is working now!");

            Console.WriteLine("Request:");
            Console.WriteLine($"Protocol: {context.Request.Protocol}");
            Console.WriteLine($"Method: {context.Request.Method}");
            Console.WriteLine($"Path: {context.Request.Path}");
            Console.WriteLine($"Content-Type: {context.Request.ContentType}");

            context.Request.EnableBuffering();
            
            using (var reader = new StreamReader(
                context.Request.Body,
                Encoding.UTF8,
                leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();
                
                if (!string.IsNullOrEmpty(body) && context.Request.ContentType?.Contains("json") == true) {
                    try {
                        var jsonElement = JsonSerializer.Deserialize<JsonElement>(body);
                        var formattedJson = JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions { 
                            WriteIndented = true 
                        });

                        Console.WriteLine($"Body:\n{formattedJson}");
                    } catch {
                        Console.WriteLine($"Body: {body}");
                    }
                } else {
                    Console.WriteLine($"Body: {body}");
                }
                
                context.Request.Body.Position = 0;
            }

            Console.WriteLine("--------");

            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            Console.WriteLine("Response:");
            Console.WriteLine($"Status code: {context.Response.StatusCode}");
            Console.WriteLine($"Content-Type: {context.Response.ContentType}");

            responseBody.Seek(0, SeekOrigin.Begin);
            var responseContent = await new StreamReader(responseBody).ReadToEndAsync();
            
            if (!string.IsNullOrEmpty(responseContent) && context.Response.ContentType?.Contains("json") == true) {
                try {
                    var jsonElement = JsonSerializer.Deserialize<JsonElement>(responseContent);
                    var formattedJson = JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions { 
                        WriteIndented = true 
                    });

                    Console.WriteLine($"Body:\n{formattedJson}");
                } catch {
                    Console.WriteLine($"Body: {responseContent}");
                }
            } else {
                Console.WriteLine($"Body: {responseContent}");
            }
            
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;

            Console.WriteLine("--------");
        }
    }
}
using System.Text;
using System.Text.Json;

// Requests/responses body writing and correct queue of requests/responses were made by AI!;

namespace GameStore.Middlewares {
    public class HttpContextWriteMiddleware {
        private readonly RequestDelegate _next;
        private readonly SemaphoreSlim _consoleLock = new(1, 1);

        public HttpContextWriteMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            var requestResponseId = Guid.NewGuid().ToString()[..8];
            
            await LogRequest(context, requestResponseId);
            
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            await LogResponse(context, responseBody, originalBodyStream, requestResponseId);
        }

        private async Task LogRequest(HttpContext context, string requestResponseId) {
            await _consoleLock.WaitAsync();
            try {
                Console.WriteLine($"\n[{requestResponseId}] ========== REQUEST: START ==========");
                Console.WriteLine($"[{requestResponseId}] Protocol: {context.Request.Protocol}");
                Console.WriteLine($"[{requestResponseId}] Method: {context.Request.Method}");
                Console.WriteLine($"[{requestResponseId}] Path: {context.Request.Path}");
                Console.WriteLine($"[{requestResponseId}] Content-Type: {context.Request.ContentType}");

                if (context.Request.ContentType?.Contains("json") == true && 
                    (context.Request.Method == "POST" || context.Request.Method == "PUT" || context.Request.Method == "PATCH")) {
                    
                    context.Request.EnableBuffering();
                    using var reader = new StreamReader(
                        context.Request.Body,
                        Encoding.UTF8,
                        leaveOpen: true);
                    
                    var body = await reader.ReadToEndAsync();
                    
                    if (!string.IsNullOrEmpty(body)) {
                        try {
                            var jsonElement = JsonSerializer.Deserialize<JsonElement>(body);
                            var formattedJson = JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions { 
                                WriteIndented = true 
                            });
                            Console.WriteLine($"[{requestResponseId}] Body:\n{formattedJson}");
                        } catch {
                            Console.WriteLine($"[{requestResponseId}] Body: {body}");
                        }
                    }
                    
                    context.Request.Body.Position = 0;
                }
                
                Console.WriteLine($"[{requestResponseId}] --------");
                Console.WriteLine($"[{requestResponseId}] ========== REQUEST: END ==========\n");
            }
            finally {
                _consoleLock.Release();
            }
        }

        private async Task LogResponse(HttpContext context, MemoryStream responseBody, Stream originalBodyStream, string requestResponseId) {
            await _consoleLock.WaitAsync();
            try {
                Console.WriteLine($"\n[{requestResponseId}] ========== RESPONSE: START ==========");
                Console.WriteLine($"[{requestResponseId}] Status code: {context.Response.StatusCode}");
                Console.WriteLine($"[{requestResponseId}] Content-Type: {context.Response.ContentType}");

                responseBody.Seek(0, SeekOrigin.Begin);
                var responseContent = await new StreamReader(responseBody).ReadToEndAsync();
                
                if (!string.IsNullOrEmpty(responseContent) && context.Response.ContentType?.Contains("json") == true) {
                    try {
                        var jsonElement = JsonSerializer.Deserialize<JsonElement>(responseContent);
                        var formattedJson = JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions { 
                            WriteIndented = true 
                        });
                        Console.WriteLine($"[{requestResponseId}] Body:\n{formattedJson}");
                    } catch {
                        Console.WriteLine($"[{requestResponseId}] Body: {responseContent}");
                    }
                } else {
                    Console.WriteLine($"[{requestResponseId}] Body: {responseContent}");
                }
                
                Console.WriteLine($"[{requestResponseId}] --------");
                Console.WriteLine($"[{requestResponseId}] ========== RESPONSE: END ==========\n");
                
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
                context.Response.Body = originalBodyStream;
            }
            finally {
                _consoleLock.Release();
            }
        }
    }
}
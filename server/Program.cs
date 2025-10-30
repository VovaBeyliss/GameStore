using Microsoft.EntityFrameworkCore;
using GameStore.Data;
using GameStore.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowAll", policy =>
        policy.WithOrigins("http://127.0.0.1:5500")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlite("Data Source=GameStore.db");
    options.EnableSensitiveDataLogging();
});

var app = builder.Build();

app.UseMiddleware<HttpContextWriteMiddleware>();

app.UseCors("AllowAll");

app.MapControllers();

using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
    Console.WriteLine("Database created");
}

app.Run();

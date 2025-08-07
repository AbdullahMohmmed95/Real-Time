using backend.Data; // Your EF DbContext
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add PostgreSQL + EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// ✅ Add controllers
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:3000", "http://62.84.190.240:8081")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// ✅ Build the app
var app = builder.Build();

// ✅ Configure middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Must be here for [ApiController] routes to work

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}



app.Run();


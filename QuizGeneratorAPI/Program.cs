


//using Microsoft.EntityFrameworkCore;
//using QuizGeneratorAPI.Data;
//using QuizGeneratorAPI.Services;
//using System.Text.Json.Serialization;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddDbContext<QuizDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("QuizDbConnection")));

//// Register QuizService as Scoped
//builder.Services.AddScoped<QuizService>();

//// Add controllers and configure JSON options to handle circular references
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Prevent circular references
//        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; // Optional: Ignore null values
//    });

//// Add Swagger services
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Enable Swagger only in development environment
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Microsoft.EntityFrameworkCore;
using QuizGeneratorAPI.Data;
using QuizGeneratorAPI.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuizDbConnection")));

// Register QuizService as Scoped
builder.Services.AddScoped<QuizService>();

// Add controllers and configure JSON options to handle circular references
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Prevent circular references
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; // Optional: Ignore null values
    });

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable serving static files from wwwroot
builder.Services.AddDirectoryBrowser(); // This is needed for directory browsing if you want to browse files from wwwroot

var app = builder.Build();

// Serve static files from wwwroot folder (for the frontend HTML, JS, and CSS)
app.UseStaticFiles();  // Serve static files (CSS, JS, HTML)

app.UseRouting();

// Enable Swagger only in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// Map controllers (for API routes)
app.MapControllers();

// Default route to serve the frontend index.html if no API route matches
app.MapFallbackToFile("index.html");

app.Run();

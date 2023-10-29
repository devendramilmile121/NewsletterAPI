using Application;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Newsletter.API.Filters;
using Newsletter.API.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Register Serilog and Seq
builder.Host.UseSerilog((context, loggerConf) =>
    loggerConf.ReadFrom.Configuration(context.Configuration)
);

// Register API Exception Filter
builder.Services.AddControllers(options =>
    options.Filters.Add<ApiExceptionFilterAttribute>())
        .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

// Dependency injection
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Health check
builder.Services.AddHealthChecks()
            .AddDbContextCheck<AppDbContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS policy
var devCorsPolicy = "devCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(devCorsPolicy);
}

app.UseAuthorization();
app.UseHealthChecks("/health");

app.MapControllers();

// seed db
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        if (context.Database.IsSqlServer())
        {
            context.Database.Migrate();
        }

        await AppDbContextInitilizer.SeedReferralDataAsync(context);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

        throw;
    }
}

app.Run();

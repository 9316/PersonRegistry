using PersonRegistry.API.Middlewares;
using PersonRegistry.Application.Extensions;
using PersonRegistry.Infrastructure.Extensions;
using PersonRegistry.Persistance.Extensions;
using PersonRegistry.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Initialize database only in Development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var dbContextInitializer = scope.ServiceProvider.GetRequiredService<PersonRegistryDbInitializer>();
    await  dbContextInitializer.InitialDataAsync();
}

// Configure the middleware pipeline
app.UseMiddleware<ExceptionHandler>();
app.UseMiddleware<LocalizationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
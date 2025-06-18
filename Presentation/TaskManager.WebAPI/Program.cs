using Serilog;
using TaskManager.Application;
using TaskManager.Infrastructure;
using TaskManager.Persistence;
using TaskManager.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerServices()
    .AddCorsServices()
    .AddJsonSerializerServices()
    .AddRateLimiterServices()
    .AddPersistenceServices(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddCachingServices(builder.Configuration);
builder.Host.UseSerilog();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerMiddleware();
}
app.UseInfrastructureServices();

app.UseCorsServices();
app.UseRateLimiter();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

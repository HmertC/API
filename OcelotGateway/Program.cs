using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
services.AddCors(o => o.AddPolicy("LowCorsPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

services.AddCors();


builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.


app.MapGet("/", () => "HelloWord");
app.UseCors("LowCorsPolicy");

app.MapControllers();
await app.UseOcelot();


app.Run();

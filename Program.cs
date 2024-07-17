using KitapService;
using KitapService.Helpers;
using KitapService.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
        ValidAudience = builder.Configuration["AppSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();
// add services to DI container
var services = builder.Services;
services.AddCors(o => o.AddPolicy("LowCorsPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
var env = builder.Environment;

services.AddDbContext<DataContext>();
services.AddCors();
services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    // ignore omitted parameters on models to enable optional params (e.g. User update)
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseDeveloperExceptionPage();
// configure HTTP request pipeline
//IApplicationBuilder applicationBuilder = app.UseRequestCounter("KitaCon");
// global cors policy
app.UseCors("LowCorsPolicy");

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();

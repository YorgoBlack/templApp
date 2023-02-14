using System.Text.Json.Serialization;
using Templ.API;
using Templ.API.Extensions;
using Templ.Infrastucture;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

services.AddEndpointsApiExplorer();

services.AddSwaggerGen();

Setup.InstallAppServices(services, builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
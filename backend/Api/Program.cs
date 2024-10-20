using Api.Extensions;
using Application.Extensions;
using Infrastructure.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
string Cors = "Cors";

builder.Services.AddInjectionInfrastructure(configuration);
builder.Services.AddInjectionApplication(configuration);
builder.Services.AddAuthentication(configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Cors,
        builder =>
        {
            _ = builder.WithOrigins("*");
            _ = builder.AllowAnyMethod();
            _ = builder.AllowAnyHeader();
        });
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseCors(Cors);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

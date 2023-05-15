using PruebaTecnica.Application.Abstraction;
using PruebaTecnica.Application.Service;
using PruebaTecnica.Domain.Abstraction;
using PruebaTecnica.Domain.Abstraction.Shared;
using PruebaTecnica.Domain.Common;
using PruebaTecnica.Persistence.Repositories;
using PruebaTecnica.Persistence.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IVehicleApplication, VehicleApplication>();
builder.Services.AddScoped<IVehicleDomain, VehicleDomain>();
builder.Services.AddScoped<IDataServiceDomain, DataServiceDomain>();
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

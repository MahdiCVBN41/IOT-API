using EquipmentManagement.API.Data;
using EquipmentManagement.API.Repositories;
using EquipmentManagement.API.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbConnectionFactory (scoped)
builder.Services.AddScoped<DbConnectionFactory>();

// Register Repositories
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IInputRepository, InputRepository>();
builder.Services.AddScoped<IValueLogRepository, ValueLogRepository>();

// Register Services
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IInputService, InputService>();
builder.Services.AddScoped<IValueLogService, ValueLogService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
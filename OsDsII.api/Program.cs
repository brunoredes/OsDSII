using Microsoft.EntityFrameworkCore;
using OsDsII.DAL.UnitOfWork;
using System.Text.Json.Serialization;
using OsDsII.api.DAL.Repositories.ServiceOrders;
using OsDsII.api.Services.ServiceOrders;
using OsDsII.api.DAL.Repositories.Customers;
using OsDsII.api.Services.Customers;
using OsDsII.api.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultMySQLConnection");
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));
    options.UseMySql(connectionString, serverVersion);
});
// Add services to the container.

builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<IServiceOrdersRepository, ServiceOrdersRepository>();
builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddScoped<IServiceOrdersService, ServiceOrdersService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddCors();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using MoneyPro2.API.Data;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder);

// Add services to the container.

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("MoneyPro2");
    builder.Services.AddDbContext<MoneyPro2DataContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });
}
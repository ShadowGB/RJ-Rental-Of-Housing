using Microsoft.EntityFrameworkCore;
using RJRentalOfHousing;
using RJRentalOfHousing.Domain;
using RJRentalOfHousing.Framework;
using RJRentalOfHousing.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

const string connectingString = "Host=127.0.0.1;Database=Chapter41;Username=postgres;Password=123456";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ApartmentDbContext>(options => options.UseNpgsql(connectingString), ServiceLifetime.Singleton);
builder.Services.AddSingleton<IUnitOfWork, EFUnitOfWork>();
builder.Services.AddScoped<ICurrencyLookup, FixedCurrencyLookup>();
builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();
builder.Services.AddScoped<ApartmentsApplicationService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.EnsureDatabase();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

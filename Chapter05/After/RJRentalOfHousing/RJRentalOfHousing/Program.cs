using Microsoft.EntityFrameworkCore;
using Npgsql;
using RJRentalOfHousing.Apartments;
using RJRentalOfHousing.Domain.Apartments;
using RJRentalOfHousing.Domain.Shared;
using RJRentalOfHousing.Domain.UserProfiles;
using RJRentalOfHousing.Framework;
using RJRentalOfHousing.Infrastructure;
using RJRentalOfHousing.UserProfiles;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

const string connectionString = "Host=127.0.0.1;Database=Chapter5;Username=postgres;Password=123456";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var purgomalumClient = new PurgomalumClient();
builder.Services.AddScoped<DbConnection>(c => new NpgsqlConnection(connectionString));
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<RentalDBContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Singleton);
builder.Services.AddSingleton<IUnitOfWork, EFUnitOfWork>();
builder.Services.AddScoped<ICurrencyLookup, FixedCurrencyLookup>();
builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<ApartmentsApplicationService>();
builder.Services.AddScoped<UserProfileApplicationService>
    (c => new UserProfileApplicationService(
        c.GetService<IUserProfileRepository>(),
        c.GetService<IUnitOfWork>(),
        text => purgomalumClient.CheckForProfanity(text).GetAwaiter().GetResult()));
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

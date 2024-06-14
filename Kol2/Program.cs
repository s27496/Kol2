using Microsoft.EntityFrameworkCore;

namespace Kol2;

public class Program
{
    public static readonly string ConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
                                                     throw new Exception(
                                                         "Environment variable DB_CONNECTION_STRING not set\nExiting...");

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();


        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(ConnectionString));

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

        // todo remove
        app.MapGet("/test" , () => "Test")
            .WithName("Test")
            .WithOpenApi();

        app.Run();
    }
}
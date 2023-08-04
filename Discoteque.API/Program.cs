using System;
using Microsoft.EntityFrameworkCore;
using Discoteque.Data;
using Discoteque.Business.IServices;
using Discoteque.Business.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database and the model is DiscotequeContext and the type of database is UseInMemoryDatabase
// base de datos en memoria
builder.Services.AddDbContext<DiscotequeContext>(
    opt => {opt.UseInMemoryDatabase("Discoteque");}
);

// inyección de dependencias, instancias
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IArtistService, ArtistService>();

var app = builder.Build();
PopulateDb(app);

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


#region  DB Population
/// <summary>
/// Populate teh Database with some data.
/// </summary>
/// <param name="app"></param>
async void PopulateDb(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var artistService = scope.ServiceProvider.GetRequiredService<IArtistService>();

        // Artists
        await artistService.CreateArtist(new Discoteque.Data.Models.Artist{
            Id = 1,
            Name = "Karol G",
            Label = "Universal Music Latin",
            IsOnTour = true
        });
        await artistService.CreateArtist(new Discoteque.Data.Models.Artist{
            Id = 2,
            Name = "Juanes",
            Label = "Universal Music Group",
            IsOnTour = true
        });
        await artistService.CreateArtist(new Discoteque.Data.Models.Artist{
            Id = 3,
            Name = "Shakira",
            Label = "Sony Music",
            IsOnTour = true
        });
        await artistService.CreateArtist(new Discoteque.Data.Models.Artist{
            Id = 4,
            Name = "Joe Arroyo",
            Label = "AVAYA",
            IsOnTour = true
        });
        await artistService.CreateArtist(new Discoteque.Data.Models.Artist{
            Id = 5,
            Name = "Carlos Vives",
            Label = "EMI/Virgin",
            IsOnTour = true
        });
        await artistService.CreateArtist(new Discoteque.Data.Models.Artist{
            Id = 6,
            Name = "Silvestre Dangond",
            Label = "SONY Music",
            IsOnTour = true
        });
        await artistService.CreateArtist(new Discoteque.Data.Models.Artist{
            Id = 7,
            Name = "Fonseca",
            Label = "SONY BMG",
            IsOnTour = true
        });
        await artistService.CreateArtist(new Discoteque.Data.Models.Artist{
            Id = 8,
            Name = "Maluma",
            Label = "RCA",
            IsOnTour = true
        });
        await artistService.CreateArtist(new Discoteque.Data.Models.Artist{
            Id = 9,
            Name = "Andrés Cepeda",
            Label = "SONY BMG",
            IsOnTour = true
        });
        await artistService.CreateArtist(new Discoteque.Data.Models.Artist{
            Id = 10,
            Name = "J Balvin",
            Label = "SONY BMG",
            IsOnTour = true
        });

    }
}
#endregion
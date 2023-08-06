using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Discoteque.Data.Models;

namespace Discoteque.Data;

public class DiscotequeContext: DbContext {

    // se hereda del sistema principal cómo se configura, el DbContext es el que hace todo lo de la bd
    public DiscotequeContext(DbContextOptions<DiscotequeContext> options): base(options) {
    }

    // Tabla donde se van a tener artistas y los albums
    public DbSet<Artist> Artists { get; set; } 
    public DbSet<Album> Albums { get; set; } 
    public DbSet<Song> Songs { get; set; } 
    public DbSet<Tour> Tours { get; set; } 

    protected override void OnModelCreating(ModelBuilder builder) {
        if (builder == null) {
            return;
        }

        builder.Entity<Artist>().ToTable("Artist").HasKey(k => k.Id);
        builder.Entity<Album>().ToTable("Album").HasKey(k => k.Id);
        builder.Entity<Song>().ToTable("Song").HasKey(k => k.Id);
        builder.Entity<Tour>().ToTable("Tour").HasKey(k => k.Id);
        base.OnModelCreating(builder);
    }

}
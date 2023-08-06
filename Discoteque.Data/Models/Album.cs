using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discoteque.Data.Models;

public class Album : BaseEntity<int> {
    /// <summary>
    /// Name of the album
    /// </summary>
    public string Name {get; set;} = "";

    /// <summary>
    /// Year the album was published
    /// </summary>
    public int Year {get; set;}
    
    /// <summary>
    /// The <see cref="Genres"/> the album belongs to
    /// </summary>
    public Genres Genre {get; set;} = Genres.Unkown;

    /// <summary>
    /// The cost of the album
    /// </summary>
    public double Cost {get; set;} = 50_000;
    
    /// <summary>
    /// The <see cref="Artist"/> id this album belongs to
    /// </summary>
    [ForeignKey("Artist")]
    public int ArtistId {get; set;}

    /// <summary>
    /// The <see cref="Artist"/> entity this album is referring to
    /// </summary>
    public virtual Artist? Artist {get; set;}
}

/// <summary>
/// A collection of musical genres
/// </summary>
public enum Genres {
    Rock,
    Metal,
    Salsa,
    Bolero,
    Merengue,
    Urban,
    Folk,
    Indie,
    Techno,
    Pop,
    Vallenato,
    Unkown
}
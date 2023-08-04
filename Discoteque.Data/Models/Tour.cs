using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discoteque.Data.Models;

public  class Tour : BaseEntity<int>
{
    /// <summary>
    /// Name of the Tour
    /// </summary>
    public string Name { get; set; } = "";
    /// <summary>
    /// Year the albums was published
    /// </summary>
    public int Year { get; set; }
    
    /// <summary>
    /// Date of the Tour 
    /// </summary>
    public DateTime TourDate { get; set; }

    /// <summary>
    /// Is it completely sold? 
    /// </summary>
    public bool IsSoldOut { get; set; }
    
    /// <summary>
    /// The <see cref="Artist"/> id this Tour belongs to
    /// </summary>
    /// <value></value>
    [ForeignKey("Id")]
    public int ArtistId { get; set; }

    /// <summary>
    /// The <see cref="Artist"/> Entity this Tour is refering to
    /// </summary> <summary>
    public virtual Artist? Artist { get; set; } 
}

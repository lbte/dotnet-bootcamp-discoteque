using System.ComponentModel.DataAnnotations.Schema;

namespace Discoteque.Data.Models;

public  class Tour : BaseEntity<int>
{
    /// <summary>
    /// Name of the Tour
    /// </summary>
    public string Name { get; set; } = "";
    
    /// <summary>
    /// City of the tour
    /// </summary>
    public string City { get; set; } = "";
    
    /// <summary>
    /// Date of the Tour 
    /// </summary>
    public DateTime TourDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Is it completely sold? 
    /// </summary>
    public bool IsSoldOut { get; set; } = false;
    
    /// <summary>
    /// The <see cref="Artist"/> id this Tour belongs to
    /// </summary>
    /// <value></value>
    [ForeignKey("Artist")]
    public int ArtistId { get; set; }

    /// <summary>
    /// The <see cref="Artist"/> Entity this Tour is refering to
    /// </summary> <summary>
    public virtual Artist? Artist { get; set; } 
}

using System.ComponentModel.DataAnnotations.Schema;

namespace Discoteque.Data.Models;

public  class Song : BaseEntity<int>
{
    /// <summary>
    /// Name of the Song
    /// </summary>
    public string Name { get; set; } = "";
    
    /// <summary>
    /// Duration of the song
    /// </summary>
    public int Length { get; set; }
    
    /// <summary>
    /// The <see cref="Album"/> id this Song belongs to
    /// </summary>
    /// <value></value>
    [ForeignKey("Album")]
    public int AlbumId { get; set; }

    /// <summary>
    /// The <see cref="Album"/> Entity this song is refering to
    /// </summary> <summary>
    public virtual Album? Album { get; set; } 
}

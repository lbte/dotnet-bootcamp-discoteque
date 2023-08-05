namespace Discoteque.Data.Models; 

// Se hereda el atributo anónimo de la clase BaseEntity, con el tipo int
// por el atributo id de la clase padre tendrá ese tipo de dato definido ahí
public class Artist : BaseEntity<int> {
    /// <summary>
    /// Name of the Artist
    /// </summary>
    public string Name {get; set;} = "";

    /// <summary>
    /// The record company where the artist publishes their work
    /// </summary>
    public string Label {get; set;} = "";
    
    /// <summary>
    /// Boolean to indicate wheter the artist is on tour or not
    /// </summary>
    public bool IsOnTour {get; set;}
}
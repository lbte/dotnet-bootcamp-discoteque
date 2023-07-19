namespace Discoteque.Data.Models; 

// Se hereda el atributo anónimo de la clase BaseEntity, con el tipo int
// por el atributo id de la clase padre tendrá ese tipo de dato definido ahí
public class Artist : BaseEntity<int> {
    public string Name {get; set;} = "";
    public string Label {get; set;} = "";
    public bool IsOnTour {get; set;}
}
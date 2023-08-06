using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Discoteque.Data.Models; // le dice a las demás clases dónde está ella

// TId: Tipo inferenciado genérico (tipo anónimo), este tipo lo va a declarar quien use la función
// TId es de tipo struct, es decir que es un componente para referenciar data
public class BaseEntity<TId> where TId : struct {
    [Key]
    public TId Id {get; set;}
}


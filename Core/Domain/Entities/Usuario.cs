using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;

public class Usuario
{
    public Usuario()
    {
        Mensajes = new HashSet<Mensaje>();
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id_Usuario { get; set; }
    public string UserName { get; set; }
    public string Contrasena { get; set; }
    
    public virtual ICollection<Mensaje> Mensajes { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;

public class Grupo
{
    public Grupo()
    {
        Mensajes = new HashSet<Mensaje>();
    }
    
    [Key]
    [DatabaseGenerated((DatabaseGeneratedOption.Identity))]
    public int Id_Grupo { get; set; }
    public string GroupName { get; set; }
    
    public virtual ICollection<Mensaje> Mensajes { get; set; }
}
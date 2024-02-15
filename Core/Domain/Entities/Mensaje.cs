using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;

public class Mensaje
{
    [Key]
    [DatabaseGenerated((DatabaseGeneratedOption.Identity))]
    public int Id_Mensaje { get; set; }
    public string Message { get; set; }
    public TimeSpan Hora { get; set; }
    public int Id_Usuario { get; set; }
    public int Id_Group { get; set; }
    
    public Grupo Grupos { get; set; }
    public Usuario Usuarios { get; set; }
}
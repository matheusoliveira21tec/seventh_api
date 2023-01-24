using System.ComponentModel.DataAnnotations;
namespace Seventh_api.Models;
public class Servidor
{
    [Key]
    [Required]
    public Guid ID { get; set; }
    [Required(ErrorMessage = "O nome do servidor é obrigatório")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O endereço IP do servidor é obrigatório")]
    public string EnderecoIP { get; set; }
    [Required(ErrorMessage = "A porta IP do servidor é obrigatória")]
    public int PortaIP { get; set; }
    public bool Disponivel { get; set; } = true;
    public ICollection<Video> Videos { get; set; } = new List<Video>();
}
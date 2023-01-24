using System.ComponentModel.DataAnnotations;
namespace Seventh_api.Models;
public class Video
{
    [Key]
    [Required]
    public Guid ID { get; set; }
    [Required(ErrorMessage = "A descrição do vídeo é obrigatória")]
    public string Descricao { get; set; }
    public string videoUrl { get; set; }

    public DateTime? DataCriacao { get; set; }

    [Required]
    public Guid IdServidor { get; set; }

    public Servidor Servidor { get; set; }
}

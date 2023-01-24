using System.ComponentModel.DataAnnotations;

namespace Seventh_api.Dtos.Videos;
public class CreateVideoDto
{
    [Required(ErrorMessage = "A descrição do vídeo é obrigatória")]
    public string Descricao { get; set; }
    public string videoUrl { get; set; }

    public DateTime? DataCriacao { get; set; } = DateTime.Now;

}

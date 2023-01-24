using System.ComponentModel.DataAnnotations;

namespace Seventh_api.Dtos.Videos;
public class UpdateVideoDto
{
    [Required(ErrorMessage = "A descrição do vídeo é obrigatória")]
    public string Descricao { get; set; }
    public string videoUrl { get; set; }

}

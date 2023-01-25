using System.ComponentModel.DataAnnotations;

namespace Seventh_api.Dtos.Videos;
public class CreateVideoDto
{
    [Required(ErrorMessage = "A descrição do vídeo é obrigatória")]
    public string Descricao { get; set; }
    [Required(ErrorMessage = "O conteúdo binário do vídeo é obrigatório")]
    public string ConteudoBinario { get; set; }
    public DateTime DataCriacao = DateTime.Today;

}

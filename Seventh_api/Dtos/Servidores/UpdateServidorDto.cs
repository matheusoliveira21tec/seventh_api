using System.ComponentModel.DataAnnotations;

namespace Seventh_api.Dtos.Servidores;
public class UpdateServidorDto
{
    [Required(ErrorMessage = "O nome do servidor é obrigatório")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O endereço IP do servidor é obrigatório")]
    public string EnderecoIP { get; set; }
    [Required(ErrorMessage = "A porta IP do servidor é obrigatória")]
    public int PortaIP { get; set; }

    public bool Disponivel = true;

}

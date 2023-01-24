using System.ComponentModel.DataAnnotations;

namespace Seventh_api.Dtos.Servidores;
public class ReadServidorDto
{
    public Guid ID { get; set; }
    public string Nome { get; set; }
    public string EnderecoIP { get; set; }
    public int PortaIP { get; set; }
    public bool Disponivel  { get; set; }

}

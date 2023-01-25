using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Seventh_api.Data;
using Seventh_api.Dtos.Servidores;
using Seventh_api.Models;

namespace Seventh_api.Controllers;

[ApiController]
public class ServidorController : ControllerBase
{
    private readonly ISeventhRepository _repository;
    private readonly IMapper _mapper;

    public ServidorController(ISeventhRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    /// <summary>
    /// Recupera todos os servidores
    /// </summary>
    /// <returns>OK</returns>
    /// <returns>BadRequest</returns>
    /// <response code="200">Caso obtenção seja feita com sucesso</response>
    /// <response code="400">Caso não exista nenhum servidor</response>
    [HttpGet("/api/servers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<ReadServidorDto>> GetServidores()
    {
        var servidores = _repository.GetAllServidores();
        if (servidores == null) return BadRequest();
        return Ok(_mapper.Map<IEnumerable<ReadServidorDto>>(servidores));
    }
    /// <summary>
    /// Adiciona um servidor ao banco de dados
    /// </summary>
    /// <param name="servidorDto">Objeto com os campos necessários para criação de um servidor</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost("api/server")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public CreatedAtActionResult CreateServidor([FromBody] CreateServidorDto servidorDto)
    {
        Servidor servidor = _mapper.Map<Servidor>(servidorDto);
        servidor.ID = Guid.NewGuid();
        _repository.CreateServidor(servidor);
        _repository.SaveChanges();
        return CreatedAtAction(nameof(GetServidor),
        new { servidorId = servidor.ID }, _mapper.Map<ReadServidorDto>(servidor));
    }
    /// <summary>
    /// Recupera um servidor
    /// </summary>
    /// <param name="servidorId">ID do servidor a ser buscado</param>
    /// <returns>OK</returns>
    /// <returns>BadRequest</returns>
    /// <response code="200">Caso obtenção seja feita com sucesso</response>
    /// <response code="400">Caso não exista servidor com o id passado</response>
    [HttpGet("api/servers/{servidorId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ReadServidorDto> GetServidor(Guid servidorId)
    {
        var servidor = _repository.GetServidor(servidorId);
        if (servidor == null) return BadRequest();
        var servidorDto = _mapper.Map<ReadServidorDto>(servidor);
        return Ok(servidorDto);
    }
    /// <summary>
    /// Retorna se um servidor está disponível
    /// </summary>
    /// <param name="servidorId">ID do servidor a ser verificado</param>
    /// <returns>OK</returns>
    /// <returns>BadRequest</returns>
    /// <response code="200">Caso verificação seja feita com sucesso, true para disponível e false para indisponível</response>
    /// <response code="400">Caso não exista servidor com o id passado</response>
    [HttpGet("/api/servers/available/{servidorId}​")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<bool> AvailableServidor(Guid servidorId)
    {
        var servidor = _repository.GetServidor(servidorId);
        if (servidor == null) return BadRequest();
        return Ok(servidor.Disponivel);
    }
    /// <summary>
    /// Atualiza um servidor
    /// </summary>
    /// <param name="servidorId">ID do servidor a ser buscado</param>
    /// <param name="servidorDto">Objeto com os campos necessários para atualização de um servidor</param>
    /// <returns>BadRequest</returns>
    /// <returns>NoContent</returns>
    /// <response code="204">Caso obtenção seja feita com sucesso</response>
    /// <response code="400">Caso não exista servidor com o id passado</response>
    [HttpPut("api/servers/{servidorId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateServidor(Guid servidorId,
[FromBody] UpdateServidorDto servidorDto)
    {
        var servidor = _repository.GetServidor(servidorId);
        if (servidor == null) return BadRequest();
        _mapper.Map(servidorDto, servidor);
        _repository.SaveChanges();
        return NoContent();
    }
    /// <summary>
    /// Deleta um servidor
    /// </summary>
    /// <param name="servidorId">ID do servidor a ser buscado</param>
    /// <returns>BadRequest</returns>
    /// <returns>NoContent</returns>
    /// <response code="204">Caso exclusão seja feita com sucesso</response>
    /// <response code="400">Caso não exista servidor com o id passado</response>
    [HttpDelete("api/servers/{servidorId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult RemoveServidor(Guid servidorId)
    {
        var servidor = _repository.GetServidor(servidorId);
        if (servidor == null) return BadRequest();
        _repository.RemoveServidor(servidor);
        _repository.SaveChanges();
        return NoContent();
    }

}

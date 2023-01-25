using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Seventh_api.Data;
using Seventh_api.Dtos.Videos;
using Seventh_api.Models;
using Seventh_api.Services;
using System;
using System.Threading;
namespace Seventh_api.Controllers;
[ApiController]
public class VideoController : ControllerBase
{
    private readonly ISeventhRepository _repository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public VideoController(ISeventhRepository repository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
        _repository = repository;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
    }
    /// <summary>
    /// Adiciona um video a um servidor
    /// </summary>
    /// <param name="servidorId">Id do servidor em que sera criado o video</param>
    /// <param name="videoDto">Objeto com os campos necessários para criação de um video</param>
    /// <returns>IActionResult</returns>
    /// <returns>BadRequest</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    /// <response code="400">Caso não exista servidor com o id passado</response>

    [HttpPost("/api/servers/{servidorId}/videos​")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ReadVideoDto> CreateVideo(Guid servidorId, CreateVideoDto videoDto)
    {
        if (!_repository.ServidorExiste(servidorId))
        {
            return BadRequest();
        }
        var video = _mapper.Map<Video>(videoDto);
        video.ID = Guid.NewGuid();
        string directorypath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadFiles");
        string filePath = Path.Combine(directorypath, video.ID.ToString());
        filePath = filePath + ".txt";
        StreamWriter sw = new StreamWriter(filePath);
        sw.WriteLine(videoDto.ConteudoBinario);
        sw.Close();
        video.videoUrl = filePath;
        _repository.CreateVideo(servidorId, video);
        _repository.SaveChanges();

        var videoReadDto = _mapper.Map<ReadVideoDto>(video);

        return CreatedAtAction(nameof(GetVideo),
        new { servidorId = servidorId, videoId = video.ID }, _mapper.Map<ReadVideoDto>(video));
    }

    /// <summary>
    /// Remove um video a um servidor
    /// </summary>
    /// <param name="servidorId">Id do servidor em que será removido o video</param>
    /// <param name="videoId">Id do video que será removido</param>
    /// <returns>NoContent</returns>
    /// <returns>BadRequest</returns>
    /// <response code="204">Caso remoção seja feita com sucesso</response>
    /// <response code="400">Caso não exista servidor ou video com o id passado</response>

    [HttpDelete("/api/servers/{servidorId}/videos/{videoId}​​")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult DeleteVideo(Guid servidorId, Guid videoId)
    {
        if (!_repository.ServidorExiste(servidorId))
        {
            return BadRequest();
        }
        if (!_repository.VideoExiste(servidorId, videoId))
        {
            return BadRequest();
        }
        string directorypath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadFiles");
        string filePath = Path.Combine(directorypath, videoId.ToString());
        filePath = filePath + ".txt";
        System.IO.File.Delete(filePath);
        _repository.RemoveVideo(servidorId, videoId);
        _repository.SaveChanges();
        return NoContent();
    }
    /// <summary>
    /// Remove os vídeo adicionados a mais de X dias, incluindo o conteúdo binário do vídeo.
    /// </summary>
    /// <param name="days">Numero de dias para excluir videos</param>
    /// <returns>Accepted</returns>
    /// <response code="202">Solicitação aceita</response>

    [HttpDelete("/api/recycler/process/{days}​​​")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<ActionResult> RecyclerVideosAsync(int days)
    {
        var videos = _repository.GetAllVideos();
        DateTime dataLimite = DateTime.Today.AddDays(-days);
        foreach (var v in videos)
        {
            if (v.DataCriacao <= dataLimite)
            {
                _repository.RemoveVideo(v.IdServidor, v.ID);
                _repository.SaveChanges();
            }
        }
        Task.Run(() => this.DeleteVideos(days,videos));
      
        return Accepted();
    }
    private void DeleteVideos(int days, IEnumerable<Video> videos)
    {
   
        Singleton.Instance.Status = "running";
        DateTime dataLimite = DateTime.Today;
        foreach (var v in videos)
        {
            if(v.DataCriacao <= dataLimite)
            {
                string directorypath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadFiles");
                string filePath = Path.Combine(directorypath, v.ID.ToString());
                filePath = filePath + ".txt";
                System.IO.File.Delete(filePath);
            }
            Thread.Sleep(20000);
        }
        Singleton.Instance.Status = "not running";

    }
    /// <summary>
    /// Recupera um video de um servidor
    /// </summary>
    /// <param name="servidorId">Id do servidor em que será recuperado o video</param>
    /// <param name="videoId">Id do video que será recuperado</param>
    /// <returns>Ok</returns>
    /// <returns>BadRequest</returns>
    /// <response code="200">Caso obtenção seja feita com sucesso</response>
    /// <response code="400">Caso não exista servidor ou video com o id passado</response>

    [HttpGet("/api/servers/{servidorId}/videos/{videoId}​​")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ReadVideoDto> GetVideo(Guid servidorId, Guid videoId)
    {
        var video = _repository.GetVideo(servidorId, videoId);
        if (video == null) return BadRequest();
        var videoDto = _mapper.Map<ReadVideoDto>(video);
        return Ok(videoDto);
    }
    /// <summary>
    /// Recupera os dados binarios de um video
    /// </summary>
    /// <param name="servidorId">Id do servidor em que serão recuperados os dados binarios</param>
    /// <param name="videoId">Id do video que serão recuperados os dados binarios</param>
    /// <returns>OK</returns>
    /// <returns>BadRequest</returns>
    /// <response code="200">Caso obtenção seja feita com sucesso</response>
    /// <response code="400">Caso não exista servidor ou video com o id passado</response>

    [HttpGet("/api/servers/{servidorId}/videos/{videoId}/binary​​​")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<string> GetBinary(Guid servidorId, Guid videoId)
    {
        if (!_repository.ServidorExiste(servidorId))
        {
            return BadRequest();
        }
        if (!_repository.VideoExiste(servidorId, videoId))
        {
            return BadRequest();
        }
        string directorypath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadFiles");
        string filePath = Path.Combine(directorypath, videoId.ToString());
        filePath = filePath + ".txt";
        StreamReader sr = new StreamReader(filePath);
        string binary = sr.ReadToEnd();
        sr.Close();
        return Ok(binary);
    }

    /// <summary>
    /// Recupera o status da reciclagem
    /// </summary>
    /// <returns>OK</returns>
    /// <response code="200">Caso obtenção seja feita com sucesso</response>
    [HttpGet("/api/recycler/status​")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string> GetStatus()
    {
            return Ok(Singleton.Instance.Status);
    }
    /// <summary>
    /// Recupera todos os videos de um servidor
    /// </summary>
    /// <param name="servidorId">Id do servidor em que será recuperado o video</param>
    /// <returns>NoContent</returns>
    /// <returns>BadRequest</returns>
    /// <response code="200">Caso obtenção seja feita com sucesso</response>
    /// <response code="400">Caso não exista servidor com o id passado</response>

    [HttpGet("/api/servers/{servidorId}/videos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ReadVideoDto> GetVideos(Guid servidorId)
    {
        var videos = _repository.GetVideos(servidorId);
        if (videos == null) return BadRequest();
        return Ok(_mapper.Map<IEnumerable<ReadVideoDto>>(videos));
    }
}

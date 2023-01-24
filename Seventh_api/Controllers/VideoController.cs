using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Seventh_api.Dtos.Videos;
using Seventh_api.Data;
using Seventh_api.Models;

namespace ItemService.Controllers;

[Route("api/item/restaurante/{restauranteId}/[controller]")]
[ApiController]
public class VideoController : ControllerBase
{
    private readonly ISeventhRepository _repository;
    private readonly IMapper _mapper;

    public VideoController(ISeventhRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReadVideoDto>> GetVideosForServidor([FromQuery]Guid servidorId)
    {

        if (!_repository.ServidorExiste(servidorId))
        {
            return NotFound();
        }

        var videos = _repository.GetVideosDeServidor(servidorId);

        return Ok(_mapper.Map<IEnumerable<ReadVideoDto>>(videos));
    }

    [HttpGet("{VideoId}", Name = "GetIVideoForServidor ")]
    public ActionResult<ReadVideoDto> GetIVideoForServidor([FromQuery]Guid servidorId, [FromQuery]Guid videoId)
    {
        if (!_repository.ServidorExiste(servidorId))
        {
            return NotFound();
        }

        var item = _repository.GetVideo(servidorId, videoId);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<ReadVideoDto>(item));
    }

    [HttpPost]
    public ActionResult<ReadVideoDto> CreateVideoForservidor([FromQuery]Guid servidorId, [FromQuery] CreateVideoDto videoDto)
    {
        if (!_repository.ServidorExiste(servidorId))
        {
            return NotFound();
        }

        var video = _mapper.Map<Video>(videoDto);

        _repository.CreateVideo(servidorId, video);
        _repository.SaveChanges();

        var videoReadDto = _mapper.Map<ReadVideoDto>(video);

        return CreatedAtRoute(nameof(GetIVideoForServidor),
            new { servidorId, VideoId = videoReadDto.Id }, videoReadDto);
    }

}

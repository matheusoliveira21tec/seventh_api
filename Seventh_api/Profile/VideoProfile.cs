using AutoMapper;
using Seventh_api.Dtos.Videos;
using Seventh_api.Models;

namespace Seventh_api.Profiles;

public class VideoProfile : Profile
{
    public VideoProfile()
    {
        CreateMap<CreateVideoDto, Video>();
        CreateMap<Video, ReadVideoDto>();
    }
}
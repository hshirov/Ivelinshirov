using AutoMapper;
using Data.Models;
using Ivelinshirov.Models;

namespace Ivelinshirov.Common.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Artwork, AdminArtworkIndexModel>();
            CreateMap<AddArtworkModel, Artwork>();
            CreateMap<AddSlideshowItemModel, Artwork>();
        }
    }
}

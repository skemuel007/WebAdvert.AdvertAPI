using AdvertAPI.Models;
using AdvertAPI.Services;
using AutoMapper;

namespace AdvertAPI.Mappings
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile() {
            CreateMap<AdvertModel, AdvertDBModel>();
        }
    }
}

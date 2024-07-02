using Assistt.Application.DTO;
using Assistt.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Application.Mapping
{
    public class GetAllProductsProfile : Profile
    {

        public GetAllProductsProfile()
        {
            CreateMap<GetAllProductsDto, Product>()
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.SilindiMi))
                .AfterMap((src, dest) => dest.AddDate = DateTime.Now)
                .ReverseMap();
        }

    }
}

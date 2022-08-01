using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;
using TestProject.ViewModels;

namespace TestProject.Mapper {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<CarModel, CarViewModel>()
                .ReverseMap()
                .ForMember(x => x.Color, opts => opts.Ignore());
            CreateMap<ColorModel, ColorViewModel>().ReverseMap();
            CreateMap<UserModel, UserViewModel>().ReverseMap();
        }
    }
}

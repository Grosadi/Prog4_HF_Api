using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfosAboutNba.Web.Models
{
    public class MapperFactory
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration( cfg =>
            {
                cfg.CreateMap<InfosAboutNba.Data.Teams, InfosAboutNba.Web.Models.Team>().
                ForMember(dest => dest.ID, map => map.MapFrom(src => src.idTeams)).
                ForMember(dest => dest.Name, map => map.MapFrom(src => src.TName)).
                ForMember(dest => dest.HomeTown, map => map.MapFrom(src => src.HomeTown)).
                ForMember(dest => dest.Found, map => map.MapFrom(src => src.Found)).
                ForMember(dest => dest.WinPercentageSinceFounded, map => map.MapFrom(src => src.WinPercentageSinceFounded)).
                ForMember(dest => dest.WinPercentageInSeason, map => map.MapFrom(src => src.WinPercentageInSeason)).
                ForMember(dest => dest.NumberOfChampionships, map => map.MapFrom(src =>
                src.NumberOfChampionships == 0 ? 0 : src.NumberOfChampionships )).ReverseMap();
            }
            );

            return config.CreateMapper();
        }
    }
}
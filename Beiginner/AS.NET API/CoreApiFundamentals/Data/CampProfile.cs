using AutoMapper;
using CoreCodeCamp.Models;
using System.Linq;

namespace CoreCodeCamp.Data
{
    public class CampProfile:Profile 
    {
        public CampProfile()
        {
            this.CreateMap<Camp,CampModel>().
                ForMember(c=>c.LocationId,o=>o.MapFrom(m=>m.Location.LocationId));
            this.CreateMap<CampModel, Camp>();

            //this.CreateMap<Camp, CampModel>().
            //    ForMember(c => c.Talks, o => o.MapFrom(m => m.Talks));
            this.CreateMap<Camp, CampModel>().ForMember(dest => dest.Talks, opt =>
                   opt.MapFrom(src => src.Talks.Select(x => x.TalkId)));

        }
    }
}

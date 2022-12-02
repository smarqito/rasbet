using AutoMapper;
using Domain;
using DTO.BetDTO;

namespace BetApplication;

public class ApplicationServicesProfile : Profile
{
    public ApplicationServicesProfile()
    {
        CreateMap<Odd, OddDTO>();
        CreateMap<Selection, SelectionDTO>();
        CreateMap<BetSimple, BetDTO>()
            .ForMember(x => x.State, o => o.MapFrom(b => b.State.ToString()))
            .ForMember(x => x.Selections, o => o.MapFrom(b => new List<Selection>(){ b.Selection }))
            .ForMember(x => x.Odd, o => o.MapFrom(b => b.Selection.Odd));
        CreateMap<BetMultiple, BetDTO>()
            .ForMember(x => x.Odd, o => o.MapFrom(b => b.OddMultiple));
    }
}

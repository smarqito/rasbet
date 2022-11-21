using AutoMapper;
using Domain;
using Domain.ResultDomain;
using DTO.GameOddDTO;

namespace GameOddApplication;

public class ApplicationServicesProfile : Profile
{
    public ApplicationServicesProfile()
    {
        CreateMap<Odd, OddDTO>();
        CreateMap<BetType, BetTypeDTO>();
        CreateMap<Game, ActiveGameDTO>();
    }
}

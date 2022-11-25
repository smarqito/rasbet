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
        CreateMap<Game, ActiveGameDTO>()
            .ForMember(x => x.MainBet, o => o.MapFrom(g => g.Bets.Where(x => x.GetType().BaseType.Name.Equals("H2h")).FirstOrDefault()));
        CreateMap<Game, GameInfoDTO>();
        CreateMap<BetType, BetInfoDTO>();
    }
}

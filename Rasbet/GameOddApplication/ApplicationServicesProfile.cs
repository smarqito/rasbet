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
        CreateMap<BetType, BetTypeDTO>()
            .ForMember(x => x.Type, o => o.MapFrom(b => b.GetType().BaseType.Name));
        CreateMap<Game, GameDTO>()
            .ForMember(x => x.MainBet, o => o.MapFrom(g => g.Bets.Where(x => x.GetType().BaseType.Name.Equals("H2h")).FirstOrDefault()))
            .ForMember(x => x.Type, o => o.MapFrom(b => b.GetType().BaseType.Name));
        CreateMap<CollectiveGame, CollectiveGameDTO>()
             .ForMember(x => x.MainBet, o => o.MapFrom(g => g.Bets.Where(x => x.GetType().BaseType.Name.Equals("H2h")).FirstOrDefault()))
             .ForMember(x => x.Type, o => o.MapFrom(b => b.GetType().BaseType.Name));
        CreateMap<Game, GameInfoDTO>();
        CreateMap<BetType, BetInfoDTO>();
        CreateMap<Sport, SportDTO>();
    }
}

using AutoMapper;
using Domain;
using Domain.ResultDomain;
using DTO.GameOddDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOddApplication;

public class ApplicationServicesProfile : Profile
{
    public ApplicationServicesProfile()
    {
        CreateMap<Odd, OddDTO>();
        CreateMap<BetType, BetTypeDTO>()
            .ForMember(b => b.Type, o => (o.GetType()).ToString());
        CreateMap<Game, ActiveGameDTO>();
    }
}

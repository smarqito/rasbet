using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOddMiddleware;

public interface IGameOddMidleware
{
    public Task UpdateBets(BetsOddsWonDTO odds);
}

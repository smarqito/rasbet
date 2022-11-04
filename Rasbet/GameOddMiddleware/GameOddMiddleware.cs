using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOddMiddleware;

public class GameOddMiddleware : IGameOddMidleware
{
    public Task UpdateBets(BetsOddsWonDTO odds)
    {
        throw new NotImplementedException();
    }
}

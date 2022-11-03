using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GameOddApplication;

public class APIService
{
    private HttpClient _httpClientBet;

    public APIService()
    {
        _httpClientBet = new();
        _httpClientBet.BaseAddress = new("http://localhost:5001/");
    }

    public async Task UpdateBets(ICollection<BetsOddsWonDTO> bets)
    {
        await _httpClientBet.PutAsJsonAsync("bet", bets);
    }
}

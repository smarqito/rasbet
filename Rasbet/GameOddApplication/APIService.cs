using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
        StringContent content = new(JsonSerializer.Serialize(bets),
                            Encoding.UTF8,
                            "application/json");
        HttpResponseMessage resp = await _httpClientBet.PutAsync($"bet", content);
        resp.EnsureSuccessStatusCode();
    }
}

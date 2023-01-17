using Domain;
using DTO;
using DTO.GameOddDTO;
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
    private HttpClient _httpClientUser;

    public APIService()
    {
        _httpClientBet = new();
        _httpClientBet.BaseAddress = new("http://localhost:5001/");
        _httpClientUser = new();
        _httpClientUser.BaseAddress = new("http://localhost:5000/");

    }

    public async Task UpdateBets(ICollection<BetsOddsWonDTO> bets)
    {
        StringContent content = new(JsonSerializer.Serialize(bets),
                            Encoding.UTF8,
                            "application/json");
        HttpResponseMessage resp = await _httpClientBet.PutAsync($"bet", content);
        resp.EnsureSuccessStatusCode();
    }

    public async Task NotifyFollowers(ChangeGameDTO changeGameDTO)
    {
        StringContent content = new(JsonSerializer.Serialize(changeGameDTO),
                            Encoding.UTF8,
                            "application/json");
        HttpResponseMessage resp = await _httpClientUser.PatchAsync($"user/notifyChangeGame", content);
        resp.EnsureSuccessStatusCode();
    }
}

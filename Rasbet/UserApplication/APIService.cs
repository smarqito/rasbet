using Domain;
using DTO.BetDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserApplication.Interfaces;

namespace UserApplication;

public class APIService
{   
    private HttpClient _httpClientBet;

    public APIService()
    {
        _httpClientBet = new ();
        _httpClientBet.BaseAddress = new("http://localhost:5001/");
    }

    public async Task<BetSimple> CreateBetSimple(CreateSimpleBetDTO bet)
    {
        StringContent content = new StringContent(JsonSerializer.Serialize(bet));
        HttpResponseMessage resp = await _httpClientBet.PostAsync($"Bet/simple", content);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<BetSimple>();
    }

    public async Task<BetMultiple> CreateBetMultiple(CreateMultipleBetDTO bets)
    {
        StringContent content = new StringContent(JsonSerializer.Serialize(bets));
        HttpResponseMessage resp = await _httpClientBet.PostAsync($"Bet/multiple", content);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<BetMultiple>();
    }

    public async Task<ICollection<BetDTO>> GetBetHistory (string userId)
    {
        StringContent content = new StringContent(JsonSerializer.Serialize(userId));
        HttpResponseMessage resp = await _httpClientBet.PostAsync($"Bet/all", content);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<ICollection<BetDTO>>();
    }
}

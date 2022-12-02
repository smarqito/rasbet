using Domain;
using Domain.ResultDomain;
using DTO.GameOddDTO;
using DTO.UserDTO;
using System.Globalization;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BetFacade;

public class APIService
{
    private HttpClient _httpClientUser;
    private HttpClient _httpClientGameOdd;

    public APIService()
    {
        _httpClientUser = new();
        _httpClientUser.BaseAddress = new("http://localhost:5000/");
        _httpClientGameOdd = new();
        _httpClientGameOdd.BaseAddress = new("http://localhost:5002/");
    }

    //HttpResponseMessage resp = await _httpClientGameOdd.PatchAsync($"gameodd/finish?gameId=1&result=1&specialistId=1", null);
    //return await resp.Content.ReadFromJsonAsync<>();
    public async Task<double> GetOdd(int betTypeId, int oddId)
    {
        HttpResponseMessage resp = await _httpClientGameOdd.GetAsync($"GameOdd/odd?betTypeId={betTypeId}&oddId={oddId}");
        resp.EnsureSuccessStatusCode();
        string s = await resp.Content.ReadAsStringAsync();

        return double.Parse(s, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
    }

    //ir buscar bettype por id
    public async Task<BetType> GetBetType(int betTypeId)
    {
        HttpResponseMessage resp = await _httpClientGameOdd.GetAsync($"GameOdd/bettype?betTypeId={betTypeId}");
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<BetType>();
    }
    
    //ir buscar bettype por id
    public async Task<GameInfoDTO> GetGame(int gameId, bool detailed)
    {
        HttpResponseMessage resp = await _httpClientGameOdd.GetAsync($"GameOdd/GameInfo?gameId={gameId}&detailed={detailed}");
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<GameInfoDTO>();
    }

    //get user by id
    public async Task<User> GetUser(string userId)
    {
        HttpResponseMessage resp = await _httpClientUser.GetAsync($"User?userId={userId}");
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<User>();
    }

    //get user simple by id
    public async Task<UserSimpleDTO> GetUserSimple(string userId)
    {
        HttpResponseMessage resp = await _httpClientUser.GetAsync($"User/userSimple?id={userId}");
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<UserSimpleDTO>();
    }


    //withdraw dinheiro do user
    public async Task WithdrawUserBalance(TransactionDTO dto, int betId)
    {
        StringContent content = new(JsonSerializer.Serialize(dto),
                                    Encoding.UTF8,
                                    "application/json");
        HttpResponseMessage resp = await _httpClientUser.PutAsync($"Wallet/withdraw", content);
        resp.EnsureSuccessStatusCode();
        HttpResponseMessage resp2 = await _httpClientUser.PatchAsync($"Wallet/bet?userId={dto.UserId}&betId={betId}", null);
        resp2.EnsureSuccessStatusCode();
    }

    //deposit dinheiro no user
    public async Task DepositUserBalance(TransactionDTO dto)
    {
        StringContent content = new(JsonSerializer.Serialize(dto),
                                    Encoding.UTF8,
                                    "application/json");
        HttpResponseMessage resp = await _httpClientUser.PutAsync($"Wallet/deposit", content);
        resp.EnsureSuccessStatusCode();
    }
}

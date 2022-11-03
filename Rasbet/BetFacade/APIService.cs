using Domain;
using Domain.ResultDomain;
using System.Net.Http.Json;
using System.Security.Cryptography;

namespace BetFacade;

public class APIService
{
    private HttpClient _httpClientUser;
    private HttpClient _httpClientGameOdd;

    public APIService()
    {
        _httpClientUser = new ();
        _httpClientUser.BaseAddress = new("http://localhost:5000/");
        _httpClientGameOdd = new ();
        _httpClientGameOdd.BaseAddress = new("http://localhost:5002/");
    }

    //HttpResponseMessage resp = await _httpClientGameOdd.PatchAsync($"gameodd/finish?gameId=1&result=1&specialistId=1", null);
    //return await resp.Content.ReadFromJsonAsync<>();
    public async Task<double> GetOdd(int betTypeId, int oddId)
    {
        HttpResponseMessage resp = await _httpClientGameOdd.GetAsync($"gameodd/odd?betTypeId={betTypeId}&oddId={oddId}");
        resp.EnsureSuccessStatusCode();
        return double.Parse(await resp.Content.ReadAsStringAsync());
    }

    //ir buscar bettype por id
    public async Task<BetType> GetBetType(int betTypeId)
    {
        HttpResponseMessage resp = await _httpClientGameOdd.GetAsync($"gameodd/bettype?betTypeId={betTypeId}");
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<BetType>();
    }

    //get user by id
    public async Task<User> GetUser(int userId)
    {
        HttpResponseMessage resp = await _httpClientUser.GetAsync($"user?userId={userId}");
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<User>();
    }

    //withdraw dinheiro do user
    public async Task<bool> WithdrawUserBalance(int userId, double amount)
    {
        StringContent content = new(amount.ToString());
        HttpResponseMessage resp = await _httpClientUser.PatchAsync($"user/withdraw?userId={userId}", content);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<bool>();
    }

    //deposit dinheiro no user
    public async Task<bool> DepositUserBalance(int userId, double amount)
    {
        StringContent content = new(amount.ToString());
        HttpResponseMessage resp = await _httpClientUser.PatchAsync($"user/deposit?userId={userId}", content);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<bool>();
    }


}
